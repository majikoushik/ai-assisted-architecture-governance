using ArchitectureGovernance.Domain.Requirements;

namespace ArchitectureGovernance.Application.Requirements.DTOs;

public record RequirementDto(
    Guid Id,
    Guid ProjectId,
    string Title,
    string RequirementText,
    string BusinessDomain,
    string? DomainContext,
    string SubmittedBy,
    string Status,
    IReadOnlyCollection<string> ExpectedArtifactTypes,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt
);
