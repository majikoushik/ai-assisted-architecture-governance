using ArchitectureGovernance.Domain.AIInteractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchitectureGovernance.Infrastructure.Persistence.Configurations;

public class AIInteractionLogConfiguration : IEntityTypeConfiguration<AIInteractionLog>
{
    public void Configure(EntityTypeBuilder<AIInteractionLog> builder)
    {
        builder.ToTable("AIInteractionLogs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProviderName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.ModelName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.PromptTemplateVersion)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.CorrelationId)
            .IsRequired()
            .HasMaxLength(100);
    }
}
