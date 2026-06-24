using ArchitectureGovernance.Domain.Projects;
using ArchitectureGovernance.Domain.Requirements;
using ArchitectureGovernance.Domain.AIInteractions;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureGovernance.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<ArchitectureProject> Projects { get; }
    DbSet<RequirementSubmission> Requirements { get; }
    DbSet<ArchitectureGovernance.Domain.Artifacts.GeneratedArtifact> Artifacts { get; }
    DbSet<ArchitectureGovernance.Domain.Reviews.ReviewRecord> Reviews { get; }
    DbSet<AIInteractionLog> AIInteractionLogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
