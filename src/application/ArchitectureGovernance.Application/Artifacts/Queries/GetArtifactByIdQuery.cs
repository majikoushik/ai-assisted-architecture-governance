using ArchitectureGovernance.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureGovernance.Application.Artifacts.Queries;

public record GetArtifactByIdQuery(Guid Id) : IRequest<ArtifactDto>;

public class GetArtifactByIdQueryHandler : IRequestHandler<GetArtifactByIdQuery, ArtifactDto>
{
    private readonly IAppDbContext _context;

    public GetArtifactByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ArtifactDto> Handle(GetArtifactByIdQuery request, CancellationToken cancellationToken)
    {
        var artifact = await _context.Artifacts
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (artifact == null)
            throw new KeyNotFoundException($"Artifact with id {request.Id} not found.");

        return ArtifactDto.FromEntity(artifact);
    }
}
