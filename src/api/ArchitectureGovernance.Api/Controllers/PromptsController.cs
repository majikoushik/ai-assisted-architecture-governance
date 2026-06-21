using ArchitectureGovernance.Application.Prompts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectureGovernance.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PromptsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PromptsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPrompts(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPromptTemplatesQuery(), cancellationToken);
        return Ok(new { Data = result });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPromptById(string id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPromptTemplateByIdQuery(id), cancellationToken);
        if (result is null)
        {
            return NotFound(new { Detail = $"Prompt template with id '{id}' was not found." });
        }
        return Ok(new { Data = result });
    }
}
