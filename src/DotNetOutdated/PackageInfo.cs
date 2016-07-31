using NuGet.Versioning;

namespace DotNetOutdated
{
    public class PackageInfo
    {
        public string Name { get; private set; }
        public SemanticVersion LowerVersion { get; private set; }
        public SemanticVersion UpperVersion { get; private set; }

        public PackageInfo(string name, SemanticVersion lower, SemanticVersion upper)
        {
            this.Name = name;
            this.LowerVersion = lower;
            this.UpperVersion = upper;
        }
    }
}