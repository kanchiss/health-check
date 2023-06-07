using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace healthcheckcoreapi.HealthChecks
{
    public class FetchProdcutHealth : IHealthCheck
    {
        private readonly IHttpClientFactory _factory;
        public FetchProdcutHealth(IHttpClientFactory factory)
        {
            _factory = factory;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using(var client = _factory.CreateClient())
            {
                var response = await client.GetAsync("https://localhost:44351/WeatherForecast/getproduct");
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(new HealthCheckResult(status: HealthStatus.Healthy, description: "getproduct API is up and running."));
                }
                return await Task.FromResult(new HealthCheckResult(status: HealthStatus.Unhealthy, description: "getproduct API is down."));
            }
        }
    }
}
