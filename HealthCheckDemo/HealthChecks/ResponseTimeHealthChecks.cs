using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HealthCheckDemo.HealthChecks
{
    public class ResponseTimeHealthChecks : IHealthCheck
    {
        private Random rnd = new Random();

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            int respomseTimeInMS = rnd.Next(1, 300);       

            if(respomseTimeInMS < 100)
            {
                return Task.FromResult(HealthCheckResult.Healthy($"Response Time is good{respomseTimeInMS}"));
            }

            else if( respomseTimeInMS < 200)
            {
                return Task.FromResult(HealthCheckResult.Degraded($"Response Time is slow{respomseTimeInMS}"));
            }
            else
            {
                return Task.FromResult(HealthCheckResult.Unhealthy($"Response Time is unacceptable{respomseTimeInMS}"));
            }

        }
    }
}
