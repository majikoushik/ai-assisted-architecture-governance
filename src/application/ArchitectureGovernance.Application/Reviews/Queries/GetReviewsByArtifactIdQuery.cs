using ArchitectureGovernance.Application.Common.Interfaces;
using ArchitectureGovernance.Application.Reviews.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureGovernance.Application.Reviews.Queries;

public record GetReviewsByArtifactIdQuery(Guid ArtifactId) : IRequest<List<ReviewDto>>;

public class GetReviewsByArtifactIdQueryHandler : IRequestHandler<GetReviewsByArtifactIdQuery, List<ReviewDto>>
{
    private readonly IAppDbContext _context;

    public GetReviewsByArtifactIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReviewDto>> Handle(GetReviewsByArtifactIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Reviews
            .Where(r => r.ArtifactId == request.ArtifactId)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new ReviewDto
            {
                Id = r.Id,
                ArtifactId = r.ArtifactId,
                ReviewerName = r.ReviewerName,
                Status = r.Status.ToString(),
                Comments = r.Comments,
                CreatedAt = r.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
