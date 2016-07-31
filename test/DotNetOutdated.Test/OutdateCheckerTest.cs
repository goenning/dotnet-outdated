using System.Linq;
using NuGet.Versioning;
using Xunit;

namespace DotNetOutdated.Test
{
    public class OutdateCheckerTest
    {
        [Fact]
        public void EmptyDependencies()
        {
            var checker = new OutdateChecker();
            var result = checker.Run(new Dependency[0]);
            Assert.Equal(0, result.Outdated.Count());
        }

        [Fact]
        public void SingleOutdatedDependency()
        {
            var checker = new OutdateChecker();
            var result = checker.Run(new Dependency[] {
                new Dependency("DotNetOutdated", SemanticVersion.Parse("0.0.1"))
            });
            Assert.Equal(1, result.Outdated.Count());
        }

        [Fact]
        public void SingleUpToDateDependency()
        {
            var checker = new OutdateChecker();
            var result = checker.Run(new Dependency[] {
                new Dependency("SharpSapRfc", SemanticVersion.Parse("2.0.10"))
            });
            Assert.Equal(0, result.Outdated.Count());
        }
    }
}