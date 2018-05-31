using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DotNetOutdated.Test
{
    public class FileSystemNuGetClient : V3NuGetClient
    {
        protected override async Task<JObject> GetResource(string name)
        {
            string content = await File.ReadAllTextAsync($"./nuget-responses/{name}");

            return JObject.Parse(content);
        }
    }
}