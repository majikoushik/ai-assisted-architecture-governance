using ArchitectureGovernance.Application.Projects.Commands;
using ArchitectureGovernance.Application.Projects.DTOs;
using ArchitectureGovernance.Application.Projects.Queries;
using ArchitectureGovernance.Application.Requirements.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectureGovernance.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}/requirements")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRequirements(Guid id)
    {
        var result = await _mediator.Send(new GetRequirementsByProjectIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProject), new { id = result.Id }, new { data = result, correlationId = HttpContext.TraceIdentifier, timestamp = DateTimeOffset.UtcNow });
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProject(Guid id)
    {
        var result = await _mediator.Send(new GetProjectByIdQuery(id));
        return Ok(new { data = result, correlationId = HttpContext.TraceIdentifier, timestamp = DateTimeOffset.UtcNow });
    }

    [HttpGet("{id:guid}/artifacts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProjectArtifacts(Guid id)
    {
        var query = new ArchitectureGovernance.Application.Artifacts.Queries.GetArtifactsByProjectIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(new { data = result, correlationId = HttpContext.TraceIdentifier, timestamp = DateTimeOffset.UtcNow });
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProjectDto>))]
    public async Task<IActionResult> GetProjects()
    {
        var result = await _mediator.Send(new GetProjectsQuery());
        return Ok(result);
    }



    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProject(Guid id, [FromBody] UpdateProjectRequest request)
    {
        var command = new UpdateProjectCommand(id, request.Name, request.BusinessDomain, request.Description, request.Owner);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPatch("{id}/status")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProjectStatus(Guid id, [FromBody] UpdateProjectStatusRequest request)
    {
        var command = new UpdateProjectStatusCommand(id, request.Status);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}

public record UpdateProjectRequest(string Name, string BusinessDomain, string Description, string Owner);
public record UpdateProjectStatusRequest(ArchitectureGovernance.Domain.Projects.ProjectStatus Status);
