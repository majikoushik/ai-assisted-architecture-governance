namespace ArchitectureGovernance.Infrastructure.Persistence;

public class DatabaseRuntimeOptions
{
    public bool IsSyntheticFallbackEnabled { get; init; }
    public string ProviderName { get; init; } = "SqlServer";
    public string? FallbackReason { get; init; }
}
