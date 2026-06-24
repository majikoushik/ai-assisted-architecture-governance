using ArchitectureGovernance.AI.Abstractions;
using ArchitectureGovernance.AI.Mock;
using ArchitectureGovernance.Application.Common.Interfaces;
using ArchitectureGovernance.Application.Prompts.Services;
using ArchitectureGovernance.Infrastructure.Filesystem;
using ArchitectureGovernance.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArchitectureGovernance.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseOptions = ResolveDatabaseOptions(configuration);
        services.AddSingleton(databaseOptions);

        services.AddDbContext<AppDbContext>(options =>
        {
            if (databaseOptions.IsSyntheticFallbackEnabled)
            {
                options.UseInMemoryDatabase("ArchitectureGovernanceSynthetic");
                return;
            }

            options.UseSqlServer(
                configuration.GetConnectionString("ArchitectureGovernance"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
        });

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

        services.Configure<PromptRepositoryOptions>(configuration.GetSection(PromptRepositoryOptions.SectionName));
        services.AddScoped<IPromptRepository, FilePromptRepository>();

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

    private static DatabaseRuntimeOptions ResolveDatabaseOptions(IConfiguration configuration)
    {
        var enableSyntheticFallback = configuration.GetValue("Database:EnableSyntheticFallback", true);
        var connectionString = configuration.GetConnectionString("ArchitectureGovernance");

        if (!enableSyntheticFallback)
        {
            return new DatabaseRuntimeOptions { ProviderName = "SqlServer" };
        }

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            return new DatabaseRuntimeOptions
            {
                IsSyntheticFallbackEnabled = true,
                ProviderName = "InMemorySynthetic",
                FallbackReason = "Connection string 'ArchitectureGovernance' is not configured."
            };
        }

        try
        {
            var builder = new SqlConnectionStringBuilder(connectionString)
            {
                ConnectTimeout = 2
            };

            if (!string.IsNullOrWhiteSpace(builder.InitialCatalog))
            {
                builder.InitialCatalog = "master";
            }

            using var connection = new SqlConnection(builder.ConnectionString);
            connection.Open();

            return new DatabaseRuntimeOptions { ProviderName = "SqlServer" };
        }
        catch (Exception ex) when (ex is SqlException or InvalidOperationException or TimeoutException)
        {
            return new DatabaseRuntimeOptions
            {
                IsSyntheticFallbackEnabled = true,
                ProviderName = "InMemorySynthetic",
                FallbackReason = $"SQL Server was not available during startup probe: {ex.Message}"
            };
        }
    }
}
