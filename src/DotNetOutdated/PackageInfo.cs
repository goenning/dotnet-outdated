using NuGet.Versioning;

namespace DotNetOutdated
{
    public class PackageInfo
    {
        public string Name { get; private set; }
        public SemanticVersion LowerVersion { get; private set; }
        public SemanticVersion UpperVersion { get; private set; }
        public SemanticVersion StableVersion { get; private set; }

        public PackageInfo(string name, string lower, string upper, string stable)
            : this(name, SemanticVersion.Parse(lower), SemanticVersion.Parse(upper), SemanticVersion.Parse(stable))
        {
        }

        public PackageInfo(string name, SemanticVersion lower, SemanticVersion upper, SemanticVersion stable)
        {
            this.Name = name;
            this.LowerVersion = lower;
            this.UpperVersion = upper;
            this.StableVersion = stable;
        }
    }
}