using ArchitectureGovernance.Domain.Projects;
using ArchitectureGovernance.Domain.Requirements;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureGovernance.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<ArchitectureProject> Projects { get; }
    DbSet<RequirementSubmission> Requirements { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
