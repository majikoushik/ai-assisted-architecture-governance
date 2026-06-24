using ArchitectureGovernance.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectureGovernance.Api.Controllers;

[ApiController]
[Route("api/v1/platform")]
public class PlatformController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly DatabaseRuntimeOptions _databaseRuntimeOptions;

    public PlatformController(IConfiguration configuration, DatabaseRuntimeOptions databaseRuntimeOptions)
    {
        _configuration = configuration;
        _databaseRuntimeOptions = databaseRuntimeOptions;
    }

    [HttpGet("readiness")]
    public IActionResult GetReadiness()
    {
        var provider = _configuration["AiProvider:Provider"] ?? "Mock";

        var response = new
        {
            service = "Architecture Governance API",
            status = "Ready",
            aiProvider = provider,
            databaseProvider = _databaseRuntimeOptions.ProviderName,
            syntheticDataMode = _databaseRuntimeOptions.IsSyntheticFallbackEnabled,
            syntheticDataReason = _databaseRuntimeOptions.FallbackReason,
            notice = "AI-assisted draft outputs require qualified architect review."
        };

        return Ok(response);
    }
}
