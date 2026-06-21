using ArchitectureGovernance.Domain.Projects;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureGovernance.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<ArchitectureProject> Projects { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
