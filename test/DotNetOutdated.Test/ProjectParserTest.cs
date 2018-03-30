using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace DotNetOutdated.Test
{
    public class ProjectParserTest
    {
        [Theory, MemberData(nameof(TestData))]
        public void ShouldGetAllDependencies(string fileName, Dependency[] expected)
        {
            // All the test projects are named ".csproj1" because for some reason the compiler want to load them when they are called just ".csproj" and errors out
            var dependencies = ProjectParser.GetAllDependencies($"./sample-projects/{fileName}.csproj1");
            Assert.Equal(expected, dependencies.ToArray());
        }

        public static IEnumerable<object[]> TestData
        {
            get
            {
                return new[]
                {
                    new object[] { "no-dependencies", new Dependency[0] },
                    new object[]
                    {
                        "single-dependency",
                        new Dependency[] { new Dependency("SomePackage", "3.10.5") }
                    },
                    new object[]
                    {
                        "tools",
                        new Dependency[] { new Dependency("DotNetOutdated", "1.0.0") }
                    },
                    new object[]
                    {
                        "framework-dependencies",
                        new Dependency[] {
                            new Dependency("SomePackage", "3.10.5"),
                            new Dependency("AnotherPackage", "2.0.0")
                        }
                    },
                    new object[]
                    {
                        "complex",
                        new Dependency[] {
                            new Dependency("SomePackage", "3.10.5"),
                            new Dependency("AnotherPackage", "2.1.0-beta"),
                            new Dependency("NuGet.Versioning", "3.5.0-beta2-1484"),
                            new Dependency("xunit", "2.2.0-beta5-build3474"),
                            new Dependency("AnotherPackage", "2.0.0"),
                            new Dependency("AnotherPackage", "3.0.0"),
                            new Dependency("DotNetOutdated", "1.0.0")
                        }
                    }
                };
            }
        }
    }
}