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
            var ws = await request.GetResponseAsync() as HttpWebResponse;

            using (var sr = new StreamReader(ws.GetResponseStream()))
            {
                JObject json = JObject.Parse(sr.ReadToEnd());
                var lower = SemanticVersion.Parse(json["items"][0]["lower"].ToString());
                var upper = SemanticVersion.Parse(json["items"][0]["upper"].ToString());
                return new PackageInfo(packageName, lower, upper);
            }
        }
    }
}