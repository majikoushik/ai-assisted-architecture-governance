using ArchitectureGovernance.Domain.Requirements;
using ArchitectureGovernance.Application.Requirements.DTOs;

namespace ArchitectureGovernance.Application.Requirements.Mappers;

public static class RequirementMapper
{
    public static RequirementDto ToDto(this RequirementSubmission requirement)
    {
        return new RequirementDto(
            requirement.Id,
            requirement.ProjectId,
            requirement.Title,
            requirement.RequirementText,
            requirement.BusinessDomain,
            requirement.DomainContext,
            requirement.SubmittedBy,
            requirement.Status.ToString(),
            requirement.ExpectedArtifactTypes.Select(a => a.ToString()).ToList(),
            requirement.CreatedAt,
            requirement.UpdatedAt
        );
    }
}
