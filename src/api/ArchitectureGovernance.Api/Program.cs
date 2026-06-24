using ArchitectureGovernance.Application;
using ArchitectureGovernance.Infrastructure;
using ArchitectureGovernance.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Observability;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var applicationInsightsConnectionString =
    builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
    ?? builder.Configuration["ApplicationInsights:ConnectionString"];

if (!string.IsNullOrWhiteSpace(applicationInsightsConnectionString))
{
    builder.Services.AddApplicationInsightsTelemetry(options =>
    {
        options.ConnectionString = applicationInsightsConnectionString;
    });
}
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ArchitectureGovernance.Infrastructure.Persistence.AppDbContext>("database", tags: new[] { "ready" });
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Architecture Governance API",
        Version = "v1",
        Description = "AI-assisted architecture governance platform — draft outputs require human review.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "Architecture Governance Team" }
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalAngularPortal", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCorrelationId();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ArchitectureGovernance.Infrastructure.Persistence.AppDbContext>();
    var databaseOptions = scope.ServiceProvider.GetRequiredService<DatabaseRuntimeOptions>();

    if (databaseOptions.IsSyntheticFallbackEnabled)
    {
        dbContext.Database.EnsureCreated();
        await SyntheticDataSeeder.SeedAsync(dbContext);
    }
    else
    {
        dbContext.Database.Migrate();
    }
}

app.UseExceptionHandler();
app.UseCorrelationId();
app.UseRequestLogging();
app.UseCors("LocalAngularPortal");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health/live", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => false // Liveness check just ensures the app is responding
});

app.MapHealthChecks("/health/ready", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.MapControllers();

app.Run();

public partial class Program;
