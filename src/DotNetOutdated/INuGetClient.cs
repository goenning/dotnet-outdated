using System.Threading.Tasks;

namespace DotNetOutdated
{
    public interface INuGetClient
    {
        Task<PackageInfo> GetPackageInfo(string packageName);
    }
}