using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DotNetOutdated
{
    public class HttpNuGetClient : V3NuGetClient
    {
        private readonly HttpClient _httpClient;

        public HttpNuGetClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected override async Task<JObject> GetResource(string name)
        {
            var response = await _httpClient.GetAsync($"https://api.nuget.org/v3/registration3/{name}");

            if (response.IsSuccessStatusCode)
            {
                return JObject.Parse(await response.Content.ReadAsStringAsync());
            }
            return null;
        }
    }
}