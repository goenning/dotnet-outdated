using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DotNetOutdated
{
    public class HttpNuGetClient : NuGetClient
    {
        protected override async Task<JObject> GetResource(string name)
        {
            var request = WebRequest.Create($"https://api.nuget.org/v3/registration1/{name}");
            var ws = await request.GetResponseAsync();
            using (var sr = new StreamReader(ws.GetResponseStream()))
            {
                return JObject.Parse(sr.ReadToEnd());
            }
        }
    }
}