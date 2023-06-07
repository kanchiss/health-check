using Microsoft.Extensions.Diagnostics.HealthChecks;
using MySql.Data.MySqlClient;

namespace healthcheckcoreapi.HealthChecks
{
    public class DatabaseHealth : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection("host=localhost;port=3306;uid=root;password=root;database=product;"))
                {
                    if (mySqlConnection.State == System.Data.ConnectionState.Open)
                    {
                        return Task.FromResult(HealthCheckResult.Healthy("Database is up and running."));
                    }
                    else
                    {
                        return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "Database is down."));
                    }
                }

            }
            catch (Exception ex)
            {
                return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "Database is down."));
            }
        }
    }
}
