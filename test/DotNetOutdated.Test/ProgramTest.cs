using NuGet.Versioning;
using Xunit;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DotNetOutdated.Test
{
    public class ProgramTest
    {
        [Theory, PropertyData("TestData")]
        public void ShouldGetAllDependencies(string fileName, Dependency[] expected)
        {
            var program = new Program();
            var dependencies = program.GetAllDependencies($"./sample-projects/{fileName}.json");
            Assert.Equal(expected, dependencies);
        }

        public static IEnumerable<object[]> TestData
        {
            get
            {
                // Or this could read from a file. :)
                return new[]
                {
                    new object[] { "empty", new Dependency[0] }
                };
            }
        }
    }
}