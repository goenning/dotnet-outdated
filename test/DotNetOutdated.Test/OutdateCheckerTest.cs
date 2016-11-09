using System.Linq;
using NuGet.Versioning;
using Xunit;

namespace DotNetOutdated.Test
{
    public class OutdateCheckerTest
    {
        private PackageInfo DotNetOutdatedPackage = new PackageInfo("DotNetOutdated", new string[] { "1.0.0", "0.0.1" });
        private PackageInfo SharpSapRfcPackage = new PackageInfo("SharpSapRfc", new string[] { "2.0.10", "1.0.0" });
        private PackageInfo SomeOtherPackagePackage = new PackageInfo("SomeOtherPackage", new string[] { "3.0.0-rc2", "2.1.0-rc1", "2.1.0", "1.0.0" });

        private OutdateChecker checker;
        private StubNuGetClient client;
        public OutdateCheckerTest()
        {
            this.client = new StubNuGetClient();
            this.client.AddPackageInfo(this.DotNetOutdatedPackage);
            this.client.AddPackageInfo(this.SharpSapRfcPackage);
            this.client.AddPackageInfo(this.SomeOtherPackagePackage);
            this.checker = new OutdateChecker(this.client);
        }

        [Fact]
        public async void EmptyDependencies()
        {
            var result = await this.checker.Run(new Dependency[0]);
            Assert.Equal(0, result.Outdated.Count());
        }

        [Fact]
        public async void SingleOutdatedDependency()
        {
            var result = await this.checker.Run(new Dependency[] {
                new Dependency("DotNetOutdated", "0.0.1")
            });
            Assert.Equal(1, result.Outdated.Count());
        }

        [Fact]
        public async void SingleUpToDateDependency()
        {
            var result = await this.checker.Run(new Dependency[] {
                new Dependency("SharpSapRfc", "2.0.10")
            });
            Assert.Equal(0, result.Outdated.Count());
        }

        [Fact]
        public async void ShouldNotWarnWhenUpperVersionIsPrereleaseDependency()
        {
            var result = await this.checker.Run(new Dependency[] {
                new Dependency("SomeOtherPackage", "2.1.0")
            });
            Assert.Equal(0, result.Outdated.Count());
        }

        [Theory]
        [InlineData("2.1.0")]
        [InlineData("2.1.0-rc1")]
        public async void WhenPreIsAllowed_ShouldWarnWhenUpperVersionIsPrereleaseDependency(string currentVersion)
        {
            var result = await this.checker.Run(new Dependency[] {
                new Dependency("SomeOtherPackage", currentVersion)
            }, allowPre: true);
            Assert.Equal(1, result.Outdated.Count());
            Assert.Equal(SemanticVersion.Parse("3.0.0-rc2"), result.Outdated.ElementAt(0).TargetVersion);
        }
    }
}