using ArchitectureGovernance.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureGovernance.Application.Artifacts.Queries;

public record GetArtifactsByRequirementIdQuery(Guid RequirementId) : IRequest<List<ArtifactDto>>;

public class GetArtifactsByRequirementIdQueryHandler : IRequestHandler<GetArtifactsByRequirementIdQuery, List<ArtifactDto>>
{
    private readonly IAppDbContext _context;

    public GetArtifactsByRequirementIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ArtifactDto>> Handle(GetArtifactsByRequirementIdQuery request, CancellationToken cancellationToken)
    {
        var artifacts = await _context.Artifacts
            .Where(a => a.RequirementSubmissionId == request.RequirementId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);

        return artifacts.Select(ArtifactDto.FromEntity).ToList();
    }
}
