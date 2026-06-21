using ArchitectureGovernance.AI.Abstractions;
using ArchitectureGovernance.AI.Mock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArchitectureGovernance.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var provider = configuration["AiProvider:Provider"] ?? "Mock";

        if (provider.Equals("Mock", StringComparison.OrdinalIgnoreCase))
        {
            services.AddSingleton<IArchitectureAiProvider, MockArchitectureAiProvider>();
        }

        return services;
    }
}
