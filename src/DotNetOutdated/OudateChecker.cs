using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetOutdated
{
    public class OutdateChecker
    {
        private INuGetClient client;
        public OutdateChecker(INuGetClient client = null)
        {
            this.client = client ?? new HttpNuGetClient();
        }

        public async Task<CheckResult> Run(IEnumerable<Dependency> dependencies)
        {
            return await this.Run(dependencies, false);
        }

        public async Task<CheckResult> Run(IEnumerable<Dependency> dependencies, bool allowPre)
        {
            var requests = dependencies.Select(x => this.client.GetPackageInfo(x.Name));

            var responses = await Task.WhenAll(requests);

            var result = new CheckResult();
            for (int i = 0; i < responses.Length; i++)
            {
                var dependency = dependencies.ElementAt(i);
                if (allowPre && dependency.CurrentVersion < responses[i].UpperVersion)
                    dependency.TargetVersion = responses[i].UpperVersion;
                else if (dependency.CurrentVersion < responses[i].StableVersion)
                    dependency.TargetVersion = responses[i].StableVersion;
                    
                if (dependency.TargetVersion != null)
                    result.AddOutdated(dependency);
            }
            return result;
        }
    }
}