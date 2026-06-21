using ArchitectureGovernance.Application;
using ArchitectureGovernance.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Observability;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCorrelationId();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ArchitectureGovernance.Infrastructure.Persistence.AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseExceptionHandler();
app.UseCorrelationId();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");
app.MapGet("/api/v1/platform/readiness", () => Results.Ok(new
{
    service = "Architecture Governance API",
    status = "Ready",
    aiProvider = "Mock",
    notice = "AI-assisted draft outputs require qualified architect review."
}))
.WithName("GetPlatformReadiness")
.WithOpenApi();

app.Run();

public partial class Program;
