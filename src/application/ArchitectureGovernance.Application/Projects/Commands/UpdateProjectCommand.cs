using ArchitectureGovernance.Application.Common.Interfaces;
using ArchitectureGovernance.Application.Projects.DTOs;
using ArchitectureGovernance.Application.Projects.Mappers;
using MediatR;

namespace ArchitectureGovernance.Application.Projects.Commands;

public record UpdateProjectCommand(Guid Id, string Name, string BusinessDomain, string Description, string Owner) : IRequest<ProjectDto>;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectDto>
{
    private readonly IAppDbContext _context;

    public UpdateProjectCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.FindAsync(new object[] { request.Id }, cancellationToken);

        if (project == null)
        {
            throw new KeyNotFoundException($"Project with id {request.Id} not found.");
        }

        project.UpdateDetails(request.Name, request.BusinessDomain, request.Description, request.Owner);

        await _context.SaveChangesAsync(cancellationToken);

        return project.ToDto();
    }
}
