using System.Collections.Generic;
using System.Linq;
using NuGet.Versioning;

namespace DotNetOutdated
{
    public class PackageInfo
    {
        public string Name { get; private set; }
        public SemanticVersion LowerVersion { get; private set; }
        public SemanticVersion UpperVersion { get; private set; }
        public SemanticVersion StableVersion { get; private set; }
        public IEnumerable<SemanticVersion> Versions { get; private set; }

        public PackageInfo(string name, string lower, string upper, string stable, IEnumerable<SemanticVersion> versions = null)
            : this(name, SemanticVersion.Parse(lower), SemanticVersion.Parse(upper), SemanticVersion.Parse(stable), versions)
        {
        }

        public PackageInfo(string name, SemanticVersion lower, SemanticVersion upper, SemanticVersion stable, IEnumerable<SemanticVersion> versions = null)
        {
            this.Name = name;
            this.LowerVersion = lower;
            this.UpperVersion = upper;
            this.StableVersion = stable;
            this.Versions = versions ?? Enumerable.Empty<SemanticVersion>();
        }
    }
}