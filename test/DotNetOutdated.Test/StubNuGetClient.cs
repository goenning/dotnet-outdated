using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DotNetOutdated.Test
{
    public class StubNuGetClient : INuGetClient
    {
        private List<PackageInfo> packages;

        public StubNuGetClient()
        {
            this.packages = new List<PackageInfo>();
        }

        public void AddPackageInfo(PackageInfo info)
        {
            this.packages.Add(info);
        }

        public async Task<PackageInfo> GetPackageInfo(string packageName)
        {
            return await Task.Run(() => {
                foreach(var package in this.packages)
                {
                    if (package.Name == packageName)
                        return package;
                }
                
                return null;
            });
        }
    }
}