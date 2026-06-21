using ArchitectureGovernance.Application.Common.Interfaces;
using ArchitectureGovernance.Domain.Reviews;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ArchitectureGovernance.Application.Reviews.Commands;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewDto>
{
    private readonly IAppDbContext _context;
    private readonly ILogger<CreateReviewCommandHandler> _logger;

    public CreateReviewCommandHandler(IAppDbContext context, ILogger<CreateReviewCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var artifact = await _context.Artifacts
            .FirstOrDefaultAsync(a => a.Id == request.ArtifactId, cancellationToken);

        if (artifact == null)
            throw new KeyNotFoundException($"Artifact with id {request.ArtifactId} not found.");

        if (!Enum.TryParse<ArchitectureGovernance.Domain.ReviewStatus>(request.ReviewStatus, out var reviewStatus))
            throw new ValidationException(new[] { new FluentValidation.Results.ValidationFailure("ReviewStatus", "Invalid review status.") });

        var correlationId = Guid.NewGuid().ToString();

        var reviewRecord = new ReviewRecord(
            artifactId: artifact.Id,
            projectId: artifact.ProjectId,
            requirementSubmissionId: artifact.RequirementSubmissionId,
            reviewerName: request.ReviewerName,
            status: reviewStatus,
            comments: request.Comments,
            correlationId: correlationId
        );

        _context.Reviews.Add(reviewRecord);

        // Update the artifact status
        artifact.UpdateStatus(reviewStatus);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Review created for Artifact {ArtifactId}. CorrelationId: {CorrelationId}", artifact.Id, correlationId);

        return new ReviewDto
        {
            Id = reviewRecord.Id,
            ArtifactId = reviewRecord.ArtifactId,
            ReviewerName = reviewRecord.ReviewerName,
            Status = reviewRecord.Status.ToString(),
            Comments = reviewRecord.Comments,
            CreatedAt = reviewRecord.CreatedAt
        };
    }
}
