using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using NuGet.Versioning;

namespace DotNetOutdated
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Program().Execute(args);
        }

        public void Execute(string[] args)
        {
            var dependencies = this.GetAllDependencies("./project.json");
            var checker = new OutdateChecker();
            var result = checker.Run(dependencies);

            if (result.Outdated.Count() > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Oh no! You have outdate dependencies.");
                foreach (var dependency in result.Outdated)
                {
                    string message = $"- {dependency.Name} is currently {dependency.CurrentVersion}, but target version is {dependency.TargetVersion}";
                    Console.WriteLine(message);
                }
            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Good Job! Your project's dependencies are all up to date!");
            }

            Console.ResetColor();
        }

        public IEnumerable<Dependency> GetAllDependencies(string filePath)
        {
            var project = File.ReadAllText(filePath);
            JObject json = JObject.Parse(project);

            var dependencies = json["dependencies"].Value<JObject>();
            if (dependencies != null)
            {
                foreach(var dependency in dependencies.Properties())
                {
                    yield return new Dependency(dependency.Name, SemanticVersion.Parse(dependency.Value.ToString()));
                }
            }       
        }
    }
}