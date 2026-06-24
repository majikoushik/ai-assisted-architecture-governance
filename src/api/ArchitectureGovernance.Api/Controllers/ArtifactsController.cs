using ArchitectureGovernance.Application.Artifacts.Commands;
using ArchitectureGovernance.Application.Artifacts.Queries;
using ArchitectureGovernance.Application.Reviews.Commands;
using ArchitectureGovernance.Application.Reviews.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectureGovernance.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ArtifactsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ArtifactsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("generate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GenerateArtifact([FromBody] GenerateArtifactCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { data = result, correlationId = result.CorrelationId, timestamp = DateTimeOffset.UtcNow });
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetArtifact(Guid id)
    {
        var result = await _mediator.Send(new GetArtifactByIdQuery(id));
        return Ok(new { data = result, correlationId = HttpContext.TraceIdentifier, timestamp = DateTimeOffset.UtcNow });
    }

    [HttpGet("{id:guid}/export/markdown")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ExportMarkdown(Guid id)
    {
        var result = await _mediator.Send(new GetArtifactByIdQuery(id));
        var bytes = System.Text.Encoding.UTF8.GetBytes(result.MarkdownContent);
        var safeTitle = string.Join("_", result.Title.Split(Path.GetInvalidFileNameChars()));
        
        return File(bytes, "text/markdown", $"{safeTitle}.md");
    }

    [HttpPost("{id:guid}/reviews")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateReview(Guid id, [FromBody] CreateReviewRequest request)
    {
        var command = new CreateReviewCommand
        {
            ArtifactId = id,
            ReviewerName = request.ReviewerName,
            ReviewStatus = request.ReviewStatus,
            Comments = request.Comments
        };
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetReviews), new { id = id }, new { data = result, correlationId = HttpContext.TraceIdentifier, timestamp = DateTimeOffset.UtcNow });
    }

    [HttpGet("{id:guid}/reviews")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReviews(Guid id)
    {
        var result = await _mediator.Send(new GetReviewsByArtifactIdQuery(id));
        return Ok(new { data = result, correlationId = HttpContext.TraceIdentifier, timestamp = DateTimeOffset.UtcNow });
    }

    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateArtifactStatusRequest request)
    {
        var command = new UpdateArtifactStatusCommand(id, request.Status, request.Reason, request.UpdatedBy);
        await _mediator.Send(command);
        return Ok(new { data = true, correlationId = HttpContext.TraceIdentifier, timestamp = DateTimeOffset.UtcNow });
    }

    [HttpGet("{id:guid}/versions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetVersions(Guid id)
    {
        var result = await _mediator.Send(new GetArtifactVersionsQuery(id));
        return Ok(new { data = result, correlationId = HttpContext.TraceIdentifier, timestamp = DateTimeOffset.UtcNow });
    }
}

public record CreateReviewRequest(string ReviewerName, string ReviewStatus, string Comments);
public record UpdateArtifactStatusRequest(string Status, string Reason, string UpdatedBy);
