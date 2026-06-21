using ArchitectureGovernance.Domain.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchitectureGovernance.Infrastructure.Persistence.Configurations;

public class ReviewRecordConfiguration : IEntityTypeConfiguration<ReviewRecord>
{
    public void Configure(EntityTypeBuilder<ReviewRecord> builder)
    {
        builder.ToTable("ReviewRecords");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.ReviewerName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(r => r.Comments)
            .HasMaxLength(4000);

        builder.Property(r => r.CorrelationId)
            .HasMaxLength(100);

        // Foreign keys
        builder.HasOne<ArchitectureGovernance.Domain.Artifacts.GeneratedArtifact>()
            .WithMany()
            .HasForeignKey(r => r.ArtifactId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<ArchitectureGovernance.Domain.Projects.ArchitectureProject>()
            .WithMany()
            .HasForeignKey(r => r.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<ArchitectureGovernance.Domain.Requirements.RequirementSubmission>()
            .WithMany()
            .HasForeignKey(r => r.RequirementSubmissionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
