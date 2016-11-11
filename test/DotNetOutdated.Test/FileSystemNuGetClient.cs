using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DotNetOutdated.Test
{
    public class FileSystemNuGetClient : NuGetClient
    {
        protected override Task<JObject> GetResource(string name)
        {
            return Task.Run(() => JObject.Parse(File.ReadAllText($"./nuget-responses/{name}")));
        }
    }
}