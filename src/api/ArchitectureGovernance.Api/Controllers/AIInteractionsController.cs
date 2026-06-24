using ArchitectureGovernance.Application.AIInteractions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectureGovernance.Api.Controllers;

[ApiController]
[Route("api/v1/ai-interactions")]
public class AIInteractionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AIInteractionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAIInteractionsQuery(), cancellationToken);
        return Ok(new { data = result, correlationId = HttpContext.TraceIdentifier, timestamp = DateTimeOffset.UtcNow });
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAIInteractionByIdQuery(id), cancellationToken);
        if (result is null)
        {
            return NotFound(new { Detail = $"AI Interaction with id '{id}' was not found." });
        }
        return Ok(new { data = result, correlationId = HttpContext.TraceIdentifier, timestamp = DateTimeOffset.UtcNow });
    }
}
