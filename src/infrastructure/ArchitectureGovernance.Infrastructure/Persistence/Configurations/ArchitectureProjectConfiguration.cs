using ArchitectureGovernance.Domain.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchitectureGovernance.Infrastructure.Persistence.Configurations;

public class ArchitectureProjectConfiguration : IEntityTypeConfiguration<ArchitectureProject>
{
    public void Configure(EntityTypeBuilder<ArchitectureProject> builder)
    {
        builder.ToTable("ArchitectureProjects");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.BusinessDomain)
            .IsRequired();

        builder.Property(t => t.Description)
            .IsRequired();

        builder.Property(t => t.Owner)
            .IsRequired();

        builder.Property(t => t.Status)
            .HasConversion<string>()
            .IsRequired();
    }
}
