using ArchitectureGovernance.Application.Common.Interfaces;
using ArchitectureGovernance.Application.Projects.DTOs;
using ArchitectureGovernance.Application.Projects.Mappers;
using ArchitectureGovernance.Domain.Projects;
using MediatR;

namespace ArchitectureGovernance.Application.Projects.Commands;

public record CreateProjectCommand(string Name, string BusinessDomain, string Description, string Owner) : IRequest<ProjectDto>;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IAppDbContext _context;

    public CreateProjectCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new ArchitectureProject(
            request.Name,
            request.BusinessDomain,
            request.Description,
            request.Owner);

        _context.Projects.Add(project);
        await _context.SaveChangesAsync(cancellationToken);

        return project.ToDto();
    }
}
