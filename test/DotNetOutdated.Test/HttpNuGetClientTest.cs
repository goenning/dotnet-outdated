using NuGet.Versioning;
using Xunit;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DotNetOutdated.Test
{
    public class HttpNuGetClientTest
    {
        [Theory, MemberData("TestData")]
        public void ShouldParseNuGetResponse(string packageName, IEnumerable<SemanticVersion> versions)
        {
            var client = new HttpNuGetClient();
            var content = File.ReadAllText($"./nuget-responses/{packageName}.json");
            var json = JObject.Parse(content);
            var package = client.ParseJson(packageName, json);
            Assert.Equal(packageName, package.Name);
            Assert.Equal(versions, package.Versions);
        }
        public static IEnumerable<object[]> TestData
        {
            get
            {
                return new[]
                {
                    new object[] { 
                        "SharpSapRfc", 
                        new List<SemanticVersion> {
                            SemanticVersion.Parse("2.0.10"),
                            SemanticVersion.Parse("2.0.9"),
                            SemanticVersion.Parse("2.0.8"),
                            SemanticVersion.Parse("2.0.5"),
                            SemanticVersion.Parse("2.0.4"),
                            SemanticVersion.Parse("2.0.3"),
                            SemanticVersion.Parse("2.0.2"),
                            SemanticVersion.Parse("2.0.1"),
                            SemanticVersion.Parse("2.0.0")
                        }
                    },
                    new object[] { 
                        "NLog", 
                        new List<SemanticVersion> {
                            SemanticVersion.Parse("4.4.0-betaV15"),	
                            SemanticVersion.Parse("4.4.0-betaV14"),	
                            SemanticVersion.Parse("4.4.0-beta13"),	
                            SemanticVersion.Parse("4.4.0-beta12"),	
                            SemanticVersion.Parse("4.4.0-beta11"),	
                            SemanticVersion.Parse("4.4.0-beta10"),	
                            SemanticVersion.Parse("4.3.6"),
                            SemanticVersion.Parse("4.3.5"),
                            SemanticVersion.Parse("4.3.4"),
                            SemanticVersion.Parse("4.3.3"),
                            SemanticVersion.Parse("4.3.2"),
                            SemanticVersion.Parse("4.3.1"),
                            SemanticVersion.Parse("4.3.0"),
                            SemanticVersion.Parse("4.2.3"),
                            SemanticVersion.Parse("4.2.2"),
                            SemanticVersion.Parse("4.2.1"),
                            SemanticVersion.Parse("4.2.0"),
                            SemanticVersion.Parse("4.1.2"),
                            SemanticVersion.Parse("4.1.1"),
                            SemanticVersion.Parse("4.1.0"),
                            SemanticVersion.Parse("4.0.1"),
                            SemanticVersion.Parse("4.0.0"),
                            SemanticVersion.Parse("3.2.1"),
                            SemanticVersion.Parse("3.2.0"),
                            SemanticVersion.Parse("3.1.0"),
                            SemanticVersion.Parse("3.0.0"),
                            SemanticVersion.Parse("2.1.0")
                        }
                    }
                };
            }
        }
    }
}