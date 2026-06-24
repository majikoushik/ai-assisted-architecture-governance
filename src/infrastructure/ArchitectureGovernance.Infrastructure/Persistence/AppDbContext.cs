using ArchitectureGovernance.Application.Common.Interfaces;
using ArchitectureGovernance.Domain.AIInteractions;
using ArchitectureGovernance.Domain.Projects;
using ArchitectureGovernance.Domain.Requirements;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ArchitectureGovernance.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ArchitectureProject> Projects => Set<ArchitectureProject>();
    public DbSet<RequirementSubmission> Requirements => Set<RequirementSubmission>();
    public DbSet<ArchitectureGovernance.Domain.Artifacts.GeneratedArtifact> Artifacts => Set<ArchitectureGovernance.Domain.Artifacts.GeneratedArtifact>();
    public DbSet<ArchitectureGovernance.Domain.Reviews.ReviewRecord> Reviews => Set<ArchitectureGovernance.Domain.Reviews.ReviewRecord>();
    public DbSet<AIInteractionLog> AIInteractionLogs => Set<AIInteractionLog>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
