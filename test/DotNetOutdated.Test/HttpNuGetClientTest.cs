using NuGet.Versioning;
using Xunit;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DotNetOutdated.Test
{
    public class HttpNuGetClientTest
    {
        [Theory]
        [InlineData("SharpSapRfc", "2.0.0", "2.0.10", "2.0.10")]
        [InlineData("NLog", "2.1.0", "4.4.0-betaV15", "4.3.6")]
        public void ShouldParseNuGetResponse(string packageName, string lower, string upper, string stable)
        {
            var client = new HttpNuGetClient();
            var content = File.ReadAllText($"./nuget-responses/{packageName}.json");
            var json = JObject.Parse(content);
            var package = client.ParseJson(packageName, json);
            Assert.Equal(packageName, package.Name);
            Assert.Equal(SemanticVersion.Parse(lower), package.LowerVersion);
            Assert.Equal(SemanticVersion.Parse(upper), package.UpperVersion);
            Assert.Equal(SemanticVersion.Parse(stable), package.StableVersion);
        }
    }
}