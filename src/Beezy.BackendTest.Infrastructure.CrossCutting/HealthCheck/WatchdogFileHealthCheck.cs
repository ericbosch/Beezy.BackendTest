using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Beezy.BackendTest.Infrastructure.CrossCutting.HealthCheck
{
    public class WhatchdogFileHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (File.Exists("watchdog.html"))
            {
                return Task.FromResult(HealthCheckResult.Healthy("Watchdog file is present"));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("Watchdog file is not present"));
        }
    }
}
