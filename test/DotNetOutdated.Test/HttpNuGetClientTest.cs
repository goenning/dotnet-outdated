using NuGet.Versioning;
using Xunit;

namespace DotNetOutdated.Test
{
    public class HttpNuGetClientTest
    {
        [Fact]
        public void GetPackageDetailsTest()
        {
            var client = new HttpNuGetClient();
            var info = client.GetPackageInfo("SharpSapRfc").Result;
            Assert.Equal("SharpSapRfc", info.Name);
            Assert.Equal(new SemanticVersion(2, 0, 0), info.LowerVersion);
            Assert.Equal(new SemanticVersion(2, 0, 10), info.UpperVersion);
        }
    }
}