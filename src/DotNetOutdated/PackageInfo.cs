using System.Collections.Generic;
using System.Linq;
using NuGet.Versioning;

namespace DotNetOutdated
{
    public class PackageInfo
    {
        public string Name { get; private set; }
        public IEnumerable<SemanticVersion> Versions { get; private set; }

        public PackageInfo(string name, IEnumerable<string> versions)
            : this(name, versions.Select(v => SemanticVersion.Parse(v)))
        {
            
        }

        public PackageInfo(string name, IEnumerable<SemanticVersion> versions)
        {
            this.Name = name;
            this.Versions = versions;
        }
    }
}