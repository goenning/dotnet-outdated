using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace DotNetOutdated.Test
{
    public class ProjectParserTest
    {
        [Theory, MemberData("TestData")]
        public void ShouldGetAllDependencies(string fileName, Dependency[] expected)
        {
            var dependencies = ProjectParser.GetAllDependencies($"./sample-projects/{fileName}.json");
            Assert.Equal(expected, dependencies.ToArray());
        }

        public static IEnumerable<object[]> TestData
        {
            get
            {
                return new[]
                {
                    new object[] { "empty", new Dependency[0] },
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
                            new Dependency("AnotherPackage", "2.0.0") ,
                            new Dependency("Microsoft.NETCore.App", "1.0.0") 
                        } 
                    },
                    new object[] 
                    { 
                        "framework-dependencies", 
                        new Dependency[] { 
                            new Dependency("SomePackage", "3.10.5"),
                            new Dependency("AnotherPackage", "2.0.0") ,
                            new Dependency("Microsoft.NETCore.App", "1.0.0") 
                        } 
                    },
                    new object[] 
                    { 
                        "complex", 
                        new Dependency[] { 
                            new Dependency("SomePackage", "3.10.5"),
                            new Dependency("AnotherPackage", "2.1.0-beta"),
                            new Dependency("NuGet.Versioning", "3.5.0-beta2-1484"),
                            new Dependency("xunit", "2.2.0-beta2-build3300"),
                            new Dependency("dotnet-test-xunit", "2.2.0-preview2-build1029"),
                            new Dependency("DotNetOutdated", "1.0.0"),
                            new Dependency("AnotherPackage", "2.0.0"),
                            new Dependency("Microsoft.NETCore.App", "1.0.0"),
                            new Dependency("AnotherPackage", "3.0.0")
                        } 
                    }
                };
            }
        }
    }
}