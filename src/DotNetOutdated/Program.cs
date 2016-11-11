using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetOutdated
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dependencies = ProjectParser.GetAllDependencies("./project.json");
            var client = new HttpNuGetClient();  
            var requests = dependencies.Select(x => client.GetPackageInfo(x.Name));
            var responses = Task.WhenAll(requests).Result;
            var data = new List<DependencyStatus>();

            for (int i = 0; i < responses.Length; i++)
            {
                var dependency = dependencies.ElementAt(i);
                var package = responses[i];
                var status = DependencyStatus.Check(dependency, package);

                data.Add(status);
            }

            data.ToStringTable(
                new[] { "Package", "Current", "Wanted", "Stable", "Latest"},
                r => {
                    if (r.Dependency.CurrentVersion < r.WantedVersion)
                        return ConsoleColor.Yellow;

                    if (r.Dependency.CurrentVersion == r.WantedVersion &&
                        r.Dependency.CurrentVersion < r.StableVersion)
                        return ConsoleColor.Red;

                    return ConsoleColor.White;
                }, 
                a => a.Package.Name, 
                a => a.Dependency.CurrentVersion, 
                a => a.WantedVersion, 
                a => a.StableVersion, 
                a => a.LatestVersion
            );
            Console.ResetColor();
        }
    }
}