using ArchitectureGovernance.Domain.Projects;
using ArchitectureGovernance.Application.Projects.DTOs;

namespace ArchitectureGovernance.Application.Projects.Mappers;

public static class ProjectMapper
{
    public static ProjectDto ToDto(this ArchitectureProject project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            BusinessDomain = project.BusinessDomain,
            Description = project.Description,
            Owner = project.Owner,
            Status = project.Status,
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt
        };
    }
}
