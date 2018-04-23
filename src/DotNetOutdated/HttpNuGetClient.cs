using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DotNetOutdated
{
    public class HttpNuGetClient : NuGetClient
    {
        protected override async Task<JObject> GetResource(string name)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://api.nuget.org/v3/registration3/{name}");

                if (response.IsSuccessStatusCode)
                {
                    return JObject.Parse(await response.Content.ReadAsStringAsync());
                }
                return null;
            }
        }
    }
}