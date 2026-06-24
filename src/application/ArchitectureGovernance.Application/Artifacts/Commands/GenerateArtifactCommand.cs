using ArchitectureGovernance.Domain;
using MediatR;

namespace ArchitectureGovernance.Application.Artifacts.Commands;

public record GenerateArtifactCommand(
    Guid ProjectId,
    Guid RequirementSubmissionId,
    ArtifactType ArtifactType
) : IRequest<ArtifactDto>;
