using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NuGet.Versioning;

namespace DotNetOutdated
{
    public class HttpNuGetClient : INuGetClient
    {
        public async Task<PackageInfo> GetPackageInfo(string packageName)
        {
            var request = WebRequest.Create($"https://api.nuget.org/v3/registration1/{packageName.ToLower()}/index.json");
            var ws = await request.GetResponseAsync();

            using (var sr = new StreamReader(ws.GetResponseStream()))
            {
                JObject json = JObject.Parse(sr.ReadToEnd());
                return ParseJson(packageName, json);
            }
        }

        public PackageInfo ParseJson(string packageName, JObject json)
        {
            SemanticVersion lower = null;
            SemanticVersion upper = null;
            SemanticVersion stable = null;
            var versions = new List<SemanticVersion>();

            var items = json["items"][0]["items"];
            foreach(var item in items) 
            {
                bool listed = Convert.ToBoolean(item["catalogEntry"]["listed"].ToString());
                if (!listed)
                    continue;

                try 
                {
                    SemanticVersion version = SemanticVersion.Parse(item["catalogEntry"]["version"].ToString());
                    versions.Add(version);

                    if (lower == null)
                        lower = version;

                    if (!version.IsPrerelease)
                        stable = version;
                    upper = version;
                } 
                catch { }
            }
            
            versions.Reverse();
            return new PackageInfo(packageName, lower, upper, stable, versions);
        }
    }
}