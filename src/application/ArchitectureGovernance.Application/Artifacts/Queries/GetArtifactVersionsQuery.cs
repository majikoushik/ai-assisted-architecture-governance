using ArchitectureGovernance.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureGovernance.Application.Artifacts.Queries;

public record GetArtifactVersionsQuery(Guid ArtifactId) : IRequest<List<ArtifactDto>>;

public class GetArtifactVersionsQueryHandler : IRequestHandler<GetArtifactVersionsQuery, List<ArtifactDto>>
{
    private readonly IAppDbContext _context;

    public GetArtifactVersionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ArtifactDto>> Handle(GetArtifactVersionsQuery request, CancellationToken cancellationToken)
    {
        var artifact = await _context.Artifacts
            .FirstOrDefaultAsync(a => a.Id == request.ArtifactId, cancellationToken);

        if (artifact == null)
            throw new KeyNotFoundException($"Artifact with id {request.ArtifactId} not found.");

        return await _context.Artifacts
            .Where(a => a.RequirementSubmissionId == artifact.RequirementSubmissionId && a.ArtifactType == artifact.ArtifactType)
            .OrderByDescending(a => a.CreatedAt)
            .Select(a => ArtifactDto.FromEntity(a))
            .ToListAsync(cancellationToken);
    }
}
