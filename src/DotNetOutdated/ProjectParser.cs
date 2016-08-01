using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using NuGet.Versioning;

namespace DotNetOutdated
{
    public class ProjectParser
    {
        public IEnumerable<Dependency> GetAllDependencies(string filePath)
        {
            HashSet<Dependency> all = new HashSet<Dependency>();
            var project = File.ReadAllText(filePath);
            JObject json = JObject.Parse(project);

            var dependencies = json["dependencies"];
            if (dependencies != null) 
            {
                foreach(var prop in dependencies.Value<JObject>().Properties())
                {
                    var dependency = Extract(prop);
                    if (dependency != null)
                        all.Add(dependency);
                } 
            }

            dependencies = json["tools"];
            if (dependencies != null) 
            {
                foreach(var prop in dependencies.Value<JObject>().Properties())
                {
                    var dependency = Extract(prop);
                    if (dependency != null)
                        all.Add(dependency);
                } 
            }

            var frameworks = json["frameworks"];
            if (frameworks != null) 
            {
                foreach(var framework in frameworks.Value<JObject>().Properties())
                {
                    if (framework.Value["dependencies"] != null) 
                    {
                        foreach(var prop in framework.Value["dependencies"].Value<JObject>().Properties())
                        {
                            var dependency = Extract(prop);
                            if (dependency != null)
                                all.Add(dependency);
                        } 
                    }
                } 
            }

            return all;
        }

        private Dependency Extract(JProperty prop)
        {
            string version = null;
            if (prop.Value.Type == JTokenType.String) 
                version = prop.Value.ToString();
            else if (prop.Value["version"] != null)
                version = prop.Value["version"].ToString();

            if (version != null)
                return new Dependency(prop.Name, version);

            return null;
        }
    }
}