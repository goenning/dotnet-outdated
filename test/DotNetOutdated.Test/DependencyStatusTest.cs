using System.Collections.Generic;
using NuGet.Versioning;
using Xunit;

namespace DotNetOutdated.Test
{
    public class DependencyStatusTest
    {

        private static PackageInfo DotNetOutdatedPackage = new PackageInfo("DotNetOutdated", new string[] { "1.0.1", "1.0.0" });
        private static PackageInfo SharpSapRfcPackage = new PackageInfo("SharpSapRfc", new string[] { "2.0.10", "1.0.2", "1.0.0" });
        private static PackageInfo SomeOtherPackagePackage = new PackageInfo("SomeOtherPackage", new string[] { "3.0.0-rc2", "2.1.3", "2.1.0-rc1", "2.1.0", "1.0.0" });

        [Theory, MemberData("TestData")]
        public void CheckVerions(PackageInfo package, string current, string wanted, string stable, string latest)
        {
            var status = DependencyStatus.Check(new Dependency(package.Name, current), package);
            Assert.Equal(SemanticVersion.Parse(wanted), status.WantedVersion);
            Assert.Equal(SemanticVersion.Parse(latest), status.LatestVersion);
            Assert.Equal(SemanticVersion.Parse(stable), status.StableVersion);
        }
        public static IEnumerable<object[]> TestData
        {
            get
            {
                return new[]
                {
                    new object[] { DotNetOutdatedPackage, "1.0.0", "1.0.1", "1.0.1", "1.0.1" },
                    new object[] { DotNetOutdatedPackage, "1.0.1", "1.0.1", "1.0.1", "1.0.1" },
                    new object[] { SharpSapRfcPackage, "1.0.0", "1.0.2", "2.0.10", "2.0.10" },
                    new object[] { SomeOtherPackagePackage, "1.0.0", "1.0.0", "2.1.3", "3.0.0-rc2" },
                    new object[] { SomeOtherPackagePackage, "2.1.0", "2.1.3", "2.1.3", "3.0.0-rc2" },
                    new object[] { SomeOtherPackagePackage, "3.0.0-rc2", "3.0.0-rc2", "2.1.3", "3.0.0-rc2" }
                };
            }
        }
    }
}