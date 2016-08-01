using NuGet.Versioning;

namespace DotNetOutdated
{
    public class Dependency
    {
        public string Name { get; private set; }
        public SemanticVersion CurrentVersion { get; private set; }
        public SemanticVersion TargetVersion { get; set; }

        public Dependency(string name, string current)
            : this(name, SemanticVersion.Parse(current))
        {
        }

        public Dependency(string name, SemanticVersion current)
        {
            this.Name = name;
            this.CurrentVersion = current;
        }
    }
}