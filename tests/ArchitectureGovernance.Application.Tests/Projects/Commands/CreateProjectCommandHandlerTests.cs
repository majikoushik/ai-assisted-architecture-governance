using ArchitectureGovernance.Application.Projects.Commands;
using ArchitectureGovernance.Infrastructure.Persistence;
using ArchitectureGovernance.Domain.Projects;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ArchitectureGovernance.Application.Tests.Projects.Commands;

public class CreateProjectCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidRequest_CreatesProject()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new AppDbContext(options);
        var handler = new CreateProjectCommandHandler(context);
        
        var command = new CreateProjectCommand(
            "Test Project",
            "Banking",
            "A test project",
            "Test Owner"
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Project", result.Name);
        
        var savedProject = await context.Projects.FirstOrDefaultAsync(p => p.Id == result.Id);
        Assert.NotNull(savedProject);
        Assert.Equal(ProjectStatus.Draft, savedProject.Status);
    }
}
