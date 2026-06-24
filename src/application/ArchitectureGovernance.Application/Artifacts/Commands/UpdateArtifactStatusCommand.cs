using ArchitectureGovernance.Application.Common.Interfaces;
using ArchitectureGovernance.Application.Reviews.Commands;
using ArchitectureGovernance.Domain.Reviews;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ArchitectureGovernance.Application.Artifacts.Commands;

public record UpdateArtifactStatusCommand(
    Guid ArtifactId,
    string Status,
    string Reason,
    string UpdatedBy
) : IRequest<bool>;

public class UpdateArtifactStatusCommandHandler : IRequestHandler<UpdateArtifactStatusCommand, bool>
{
    private readonly IAppDbContext _context;
    private readonly ILogger<UpdateArtifactStatusCommandHandler> _logger;

    public UpdateArtifactStatusCommandHandler(IAppDbContext context, ILogger<UpdateArtifactStatusCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdateArtifactStatusCommand request, CancellationToken cancellationToken)
    {
        var artifact = await _context.Artifacts
            .FirstOrDefaultAsync(a => a.Id == request.ArtifactId, cancellationToken);

        if (artifact == null)
            throw new KeyNotFoundException($"Artifact with id {request.ArtifactId} not found.");

        if (!Enum.TryParse<ArchitectureGovernance.Domain.ReviewStatus>(request.Status, out var reviewStatus))
            throw new ValidationException(new[] { new FluentValidation.Results.ValidationFailure("Status", "Invalid review status.") });

        // Update the artifact status
        artifact.UpdateStatus(reviewStatus);

        // Also create a review record to track this status change
        var correlationId = Guid.NewGuid().ToString();
        var reviewRecord = new ReviewRecord(
            artifactId: artifact.Id,
            projectId: artifact.ProjectId,
            requirementSubmissionId: artifact.RequirementSubmissionId,
            reviewerName: request.UpdatedBy,
            status: reviewStatus,
            comments: request.Reason,
            correlationId: correlationId
        );

        _context.Reviews.Add(reviewRecord);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Artifact {ArtifactId} status updated to {Status} by {UpdatedBy}. CorrelationId: {CorrelationId}", 
            artifact.Id, reviewStatus, request.UpdatedBy, correlationId);

        return true;
    }
}
