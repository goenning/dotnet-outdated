using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DotNetOutdated
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string firstProjectFile = Directory.EnumerateFiles("./").FirstOrDefault(x => Path.GetExtension(x) == ".csproj");

            if (firstProjectFile == null)
            {
                Console.WriteLine("No project file found");
                return;
            }
            
            var data = new List<DependencyStatus>();

            using (var httpClient = new HttpClient())
            {
                var dependencies = ProjectParser.GetAllDependencies(firstProjectFile);
                var client = new HttpNuGetClient(httpClient);
                var requests = dependencies.Select(x => client.GetPackageInfo(x.Name));
                var responses = Task.WhenAll(requests).Result.Where(response => response != null).ToArray();
                for (int i = 0; i < responses.Length; i++)
                {
                    var dependency = dependencies.ElementAt(i);
                    var package = responses[i];
                    var status = DependencyStatus.Check(dependency, package);

                    if (status.LatestVersion > status.Dependency.CurrentVersion)
                    {
                        data.Add(status);
                    }
                }
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