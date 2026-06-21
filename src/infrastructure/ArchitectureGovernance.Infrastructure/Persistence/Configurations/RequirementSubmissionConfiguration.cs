using ArchitectureGovernance.Domain.Requirements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchitectureGovernance.Infrastructure.Persistence.Configurations;

public class RequirementSubmissionConfiguration : IEntityTypeConfiguration<RequirementSubmission>
{
    public void Configure(EntityTypeBuilder<RequirementSubmission> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.RequirementText)
            .IsRequired()
            .HasMaxLength(10000);

        builder.Property(x => x.BusinessDomain)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.DomainContext)
            .HasMaxLength(500);

        builder.Property(x => x.SubmittedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        // EF Core 8 primitive collections natively map to JSON arrays.
        // To store the enums as string, we can't easily configure a value converter for the elements of a primitive collection yet,
        // so it will be stored as an array of integers representing the enum. This is acceptable for the MVP.

        // Relationship
        builder.HasOne(x => x.Project)
            .WithMany(p => p.Requirements)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
