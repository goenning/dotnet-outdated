using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DotNetOutdated.Test
{
    public class MissingPackageNuGetClient : V3NuGetClient
    {
        protected override Task<JObject> GetResource(string name) => Task.FromResult((JObject)null);
    }
}