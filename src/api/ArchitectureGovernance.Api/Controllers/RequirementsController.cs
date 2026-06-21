using ArchitectureGovernance.Application.Requirements.Commands;
using ArchitectureGovernance.Application.Requirements.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectureGovernance.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RequirementsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RequirementsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateRequirementCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, new { data = result, correlationId = HttpContext.TraceIdentifier, timestamp = DateTimeOffset.UtcNow });
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetRequirementByIdQuery(id));
        return Ok(new { data = result, correlationId = HttpContext.TraceIdentifier, timestamp = DateTimeOffset.UtcNow });
    }

    [HttpGet("{id:guid}/artifacts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRequirementArtifacts(Guid id)
    {
        var query = new ArchitectureGovernance.Application.Artifacts.Queries.GetArtifactsByRequirementIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(new { data = result, correlationId = HttpContext.TraceIdentifier, timestamp = DateTimeOffset.UtcNow });
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllRequirementsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetRequirementByIdQuery(id));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRequirementCommand command)
    {
        if (id != command.Id) return BadRequest();
        
        var result = await _mediator.Send(command);
        if (result == null) return NotFound();
        
        return NoContent();
    }

    [HttpPatch("{id}/status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateRequirementStatusCommand command)
    {
        if (id != command.Id) return BadRequest();
        
        var result = await _mediator.Send(command);
        if (result == null) return NotFound();
        
        return Ok(result);
    }
}
