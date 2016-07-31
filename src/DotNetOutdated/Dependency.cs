using NuGet.Versioning;

namespace DotNetOutdated
{
    public class Dependency
    {
        public string Name { get; private set; }
        public SemanticVersion CurrentVersion { get; private set; }
        public SemanticVersion UpperVersion { get; set; }

        public Dependency(string name, SemanticVersion current)
        {
            this.Name = name;
            this.CurrentVersion = current;
        }
    }
}