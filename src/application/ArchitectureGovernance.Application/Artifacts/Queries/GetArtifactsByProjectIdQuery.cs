using ArchitectureGovernance.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureGovernance.Application.Artifacts.Queries;

public record GetArtifactsByProjectIdQuery(Guid ProjectId) : IRequest<List<ArtifactDto>>;

public class GetArtifactsByProjectIdQueryHandler : IRequestHandler<GetArtifactsByProjectIdQuery, List<ArtifactDto>>
{
    private readonly IAppDbContext _context;

    public GetArtifactsByProjectIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ArtifactDto>> Handle(GetArtifactsByProjectIdQuery request, CancellationToken cancellationToken)
    {
        var artifacts = await _context.Artifacts
            .Where(a => a.ProjectId == request.ProjectId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);

        return artifacts.Select(ArtifactDto.FromEntity).ToList();
    }
}
