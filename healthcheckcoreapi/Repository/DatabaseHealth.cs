using Microsoft.Extensions.Diagnostics.HealthChecks;
using MySql.Data.MySqlClient;

namespace healthcheckcoreapi.Repository
{
    public class DatabaseHealth : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using (MySqlConnection mySqlConnection =
                     new MySqlConnection("User Id = postgres; Password = sa123#;" +
                     "host=localhost;database=Demo;"))
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
