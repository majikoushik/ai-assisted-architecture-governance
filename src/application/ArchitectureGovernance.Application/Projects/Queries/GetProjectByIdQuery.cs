using ArchitectureGovernance.Application.Common.Interfaces;
using ArchitectureGovernance.Application.Projects.DTOs;
using ArchitectureGovernance.Application.Projects.Mappers;
using MediatR;

namespace ArchitectureGovernance.Application.Projects.Queries;

public record GetProjectByIdQuery(Guid Id) : IRequest<ProjectDto?>;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto?>
{
    private readonly IAppDbContext _context;

    public GetProjectByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectDto?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.FindAsync(new object[] { request.Id }, cancellationToken);

        return project?.ToDto();
    }
}
