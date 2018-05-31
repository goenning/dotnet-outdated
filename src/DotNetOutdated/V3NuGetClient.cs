using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;

namespace DotNetOutdated
{
    public class V3NugetClient 
    {
        private readonly HttpClient httpClient;

        public V3NugetClient(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<PackageInfo> GetPackageInfo(string name)
        {
            var logger = new Logger();
            var providers = new List<Lazy<INuGetResourceProvider>>();
            providers.AddRange(Repository.Provider.GetCoreV3());
            var packageSource = new PackageSource("https://api.nuget.org/v3/index.json");
            var sourceRepository = new SourceRepository(packageSource, providers);

            var packageMetadataResource = await sourceRepository.GetResourceAsync<PackageMetadataResource>();
            IEnumerable<IPackageSearchMetadata> searchMetadata = await packageMetadataResource.GetMetadataAsync(name, true, true, new SourceCacheContext(), logger, CancellationToken.None);

            if(!searchMetadata.Any())
            {
                return null;
            }

            var versions = searchMetadata.Select(package => package.Identity.Version).ToList();

            return new PackageInfo(name, versions);
        }

        public class Logger : ILogger
        {
            public void Log(LogLevel level, string data)
            {
            }

            public void Log(ILogMessage message)
            {
            }

            public Task LogAsync(LogLevel level, string data)
            {
                return Task.CompletedTask;
            }

            public Task LogAsync(ILogMessage message)
            {
                return Task.CompletedTask;
            }

            public void LogDebug(string data)
            {
            }

            public void LogError(string data)
            {
            }

            public void LogInformation(string data)
            {
            }

            public void LogInformationSummary(string data)
            {
            }

            public void LogMinimal(string data)
            {
            }

            public void LogVerbose(string data)
            {
            }

            public void LogWarning(string data)
            {
            }
        }
    }
}