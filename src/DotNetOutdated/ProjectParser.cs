using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DotNetOutdated
{
    public static class ProjectParser
    {
        public static IEnumerable<Dependency> GetAllDependencies(string filePath)
        {
            var all = new HashSet<Dependency>();
            var project = File.ReadAllText(filePath);
            var document = XDocument.Parse(project);

            var dependencies = document.Descendants("PackageReference");

            foreach (var package in dependencies)
            {
                Dependency dependency = Extract(package);
                
                if (dependency != null)
                {
                    all.Add(dependency);
                }
            }

            dependencies = document.Descendants("DotNetCliToolReference");

            foreach (var package in dependencies)
            {
                Dependency dependency = Extract(package);
                
                if (dependency != null)
                {
                    all.Add(dependency);
                }
            }

            return all;
        }

        private static Dependency Extract(XElement element)
        {
            string name = element.Attribute("Include")?.Value;
            string version = element.Attribute("Version")?.Value;

            if (name != null && version != null)
            {
                return new Dependency(name, version);
            }

            return null;
        }
    }
}