using ArchitectureGovernance.AI.Abstractions;
using ArchitectureGovernance.AI.Mock;
using ArchitectureGovernance.Application.Common.Interfaces;
using ArchitectureGovernance.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArchitectureGovernance.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("ArchitectureGovernance"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

        services.AddSingleton<ArchitectureGovernance.Application.Prompts.Services.IPromptRepository, ArchitectureGovernance.Infrastructure.Filesystem.FilePromptRepository>();

        var provider = configuration["AiProvider:Provider"] ?? "Mock";
        
        services.Configure<ArchitectureGovernance.AI.AzureOpenAI.AzureOpenAiOptions>(
            configuration.GetSection(ArchitectureGovernance.AI.AzureOpenAI.AzureOpenAiOptions.SectionName));

        if (provider.Equals("AzureOpenAI", StringComparison.OrdinalIgnoreCase))
        {
            services.AddSingleton<IArchitectureAiProvider, ArchitectureGovernance.AI.AzureOpenAI.AzureOpenAiProvider>();
        }
        else
        {
            services.AddSingleton<IArchitectureAiProvider, MockArchitectureAiProvider>();
        }

        return services;
    }
}
