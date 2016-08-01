using System.Linq;
using NuGet.Versioning;
using Xunit;

namespace DotNetOutdated.Test
{
    public class OutdateCheckerTest
    {
        private OutdateChecker checker;
        private StubNuGetClient client;
        public OutdateCheckerTest()
        {
            this.client = new StubNuGetClient();
            this.client.AddPackageInfo(new PackageInfo("DotNetOutdated","0.0.1", "1.0.0", "1.0.0"));
            this.client.AddPackageInfo(new PackageInfo("SharpSapRfc", "1.0.0", "2.0.10", "2.0.10"));
            this.client.AddPackageInfo(new PackageInfo("SomeOtherPackage", "1.0.0", "3.0.0-rc2", "2.1.0"));
            this.checker = new OutdateChecker(this.client);
        }

        [Fact]
        public void EmptyDependencies()
        {
            var result = this.checker.Run(new Dependency[0]);
            Assert.Equal(0, result.Outdated.Count());
        }

        [Fact]
        public void SingleOutdatedDependency()
        {
            var result = this.checker.Run(new Dependency[] {
                new Dependency("DotNetOutdated", "0.0.1")
            });
            Assert.Equal(1, result.Outdated.Count());
        }

        [Fact]
        public void SingleUpToDateDependency()
        {
            var result = this.checker.Run(new Dependency[] {
                new Dependency("SharpSapRfc", "2.0.10")
            });
            Assert.Equal(0, result.Outdated.Count());
        }

        [Fact]
        public void ShouldNotWarnWhenUpperVersionIsPrereleaseDependency()
        {
            var result = this.checker.Run(new Dependency[] {
                new Dependency("SomeOtherPackage", "2.1.0")
            });
            Assert.Equal(0, result.Outdated.Count());
        }
    }
}