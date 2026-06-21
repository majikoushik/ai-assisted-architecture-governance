using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ArchitectureGovernance.Api.Controllers;

[ApiController]
[Route("api/v1/platform")]
public class PlatformController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public PlatformController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("readiness")]
    public IActionResult GetReadiness()
    {
        var provider = _configuration["AiProvider:Provider"] ?? "Mock";

        var response = new
        {
            Status = "Healthy",
            AiProvider = provider
        };

        return Ok(response);
    }
}
