using System.Collections.Generic;

namespace DotNetOutdated
{
    public class OutdateChecker
    {
        private INuGetClient client;
        public OutdateChecker(INuGetClient client = null)
        {
            this.client = client ?? new HttpNuGetClient();
        }

        public CheckResult Run(IEnumerable<Dependency> dependencies)
        {
            var result = new CheckResult();
            foreach (var dependency in dependencies ?? new Dependency[0])
            {
                var info = this.client.GetPackageInfo(dependency.Name).Result;
                if (dependency.CurrentVersion < info.UpperVersion)
                {
                    dependency.UpperVersion = info.UpperVersion;
                    result.AddOutdated(dependency);
                }
            }
            return result;
        }
    }
}