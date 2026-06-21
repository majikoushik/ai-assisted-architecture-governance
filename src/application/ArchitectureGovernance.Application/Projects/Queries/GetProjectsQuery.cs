using ArchitectureGovernance.Application.Common.Interfaces;
using ArchitectureGovernance.Application.Projects.DTOs;
using ArchitectureGovernance.Application.Projects.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureGovernance.Application.Projects.Queries;

public record GetProjectsQuery : IRequest<List<ProjectDto>>;

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, List<ProjectDto>>
{
    private readonly IAppDbContext _context;

    public GetProjectsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _context.Projects
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return projects.Select(p => p.ToDto()).ToList();
    }
}
