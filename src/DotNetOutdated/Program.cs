using System;
using System.Linq;

namespace DotNetOutdated
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var allowPre = args.Any(a => a.Equals("-pre"));
            var parser = new ProjectParser();
            var checker = new OutdateChecker();
            var dependencies = parser.GetAllDependencies("./project.json");
            var result = checker.Run(dependencies, allowPre).Result;

            if (result.Outdated.Count() > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Oh no! You have outdated dependencies.");
                foreach (var dependency in result.Outdated)
                {
                    string name = dependency.TargetVersion.IsPrerelease ? "latest (pre)" : "stable";
                    string message = $"- {dependency.Name} is currently {dependency.CurrentVersion}, but {name} version is {dependency.TargetVersion}";
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
    }
}