using NuGet.Versioning;
using System.Linq;

namespace DotNetOutdated
{
    public class DependencyStatus
    {
        public PackageInfo Package { get; private set; }
        public Dependency Dependency { get; private set; }
        public SemanticVersion WantedVersion { get; private set; }
        public SemanticVersion StableVersion { get; private set; }
        public SemanticVersion LatestVersion { get; private set; }

        private DependencyStatus()
        {

        }

        public static DependencyStatus Check(Dependency dependency, PackageInfo package)
        {
            var status = new DependencyStatus();
            status.Dependency = dependency;
            status.Package = package;

            foreach(var version in package.Versions)
            {
                if (status.LatestVersion == null)
                    status.LatestVersion = version;

                if (!version.IsPrerelease && status.StableVersion == null)
                {
                    status.WantedVersion = version;
                    status.StableVersion = version;
                }
            }

            if (dependency.CurrentVersion > status.WantedVersion)
                status.WantedVersion = dependency.CurrentVersion;

            if (status.WantedVersion.Major > dependency.CurrentVersion.Major) 
                status.WantedVersion = package.Versions.FirstOrDefault(x => !x.IsPrerelease && x.Major == dependency.CurrentVersion.Major);

            return status;
        }
    }
}