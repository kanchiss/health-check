using healthcheckcoreapi.Models;
using healthcheckcoreapi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace healthcheckcoreapi.Controllers
{
    public class HealthCheckController : Controller
    {
        private readonly ILogger<HealthCheckController> _logger;
        private readonly HealthCheckService _service;
        public HealthCheckController(ILogger<HealthCheckController> logger,
        HealthCheckService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var report = await _service.CheckHealthAsync();
            string json = System.Text.Json.JsonSerializer.Serialize(report);

            if (report.Status == HealthStatus.Healthy)
                return Ok(json);
            return NotFound("Service unavailable");
        }
    }
}
