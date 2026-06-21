using MediatR;

namespace ArchitectureGovernance.Application.Artifacts.Commands;

public record GenerateArtifactCommand(
    Guid ProjectId,
    Guid RequirementSubmissionId,
    string ArtifactType
) : IRequest<ArtifactDto>;
