using ArchitectureGovernance.Domain.Artifacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchitectureGovernance.Infrastructure.Persistence.Configurations;

public class GeneratedArtifactConfiguration : IEntityTypeConfiguration<GeneratedArtifact>
{
    public void Configure(EntityTypeBuilder<GeneratedArtifact> builder)
    {
        builder.ToTable("GeneratedArtifacts");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.ArtifactType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(100);

        builder.Property(a => a.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(a => a.ProviderName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.PromptTemplateName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.PromptTemplateVersion)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.CorrelationId)
            .HasMaxLength(100);

        // MarkdownContent could be large
        builder.Property(a => a.MarkdownContent)
            .IsRequired();

        // Foreign keys (No explicit navigation properties in domain to keep it simple, just strict IDs)
        builder.HasOne<ArchitectureGovernance.Domain.Projects.ArchitectureProject>()
            .WithMany()
            .HasForeignKey(a => a.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<ArchitectureGovernance.Domain.Requirements.RequirementSubmission>()
            .WithMany()
            .HasForeignKey(a => a.RequirementSubmissionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
