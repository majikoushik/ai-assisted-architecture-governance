using ArchitectureGovernance.Application.Common.Interfaces;
using ArchitectureGovernance.Application.Projects.DTOs;
using ArchitectureGovernance.Application.Projects.Mappers;
using ArchitectureGovernance.Domain.Projects;
using MediatR;

namespace ArchitectureGovernance.Application.Projects.Commands;

public record UpdateProjectStatusCommand(Guid Id, ProjectStatus Status) : IRequest<ProjectDto>;

public class UpdateProjectStatusCommandHandler : IRequestHandler<UpdateProjectStatusCommand, ProjectDto>
{
    private readonly IAppDbContext _context;

    public UpdateProjectStatusCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectDto> Handle(UpdateProjectStatusCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.FindAsync(new object[] { request.Id }, cancellationToken);

        if (project == null)
        {
            throw new KeyNotFoundException($"Project with id {request.Id} not found.");
        }

        project.UpdateStatus(request.Status);

        await _context.SaveChangesAsync(cancellationToken);

        return project.ToDto();
    }
}
