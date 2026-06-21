using ArchitectureGovernance.AI.Abstractions;
using Xunit;
using ArchitectureGovernance.Application.Artifacts.Commands;
using ArchitectureGovernance.Application.Common.Interfaces;
using ArchitectureGovernance.Application.Prompts.Services;
using ArchitectureGovernance.Domain.Artifacts;
using ArchitectureGovernance.Domain.Projects;
using ArchitectureGovernance.Domain.Prompts;
using ArchitectureGovernance.Domain.Requirements;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace ArchitectureGovernance.Application.Tests.Artifacts.Commands;

public class GenerateArtifactCommandHandlerTests
{
    private readonly Mock<IAppDbContext> _contextMock;
    private readonly Mock<IArchitectureAiProvider> _aiProviderMock;
    private readonly Mock<IPromptRepository> _promptRepoMock;

    public GenerateArtifactCommandHandlerTests()
    {
        _contextMock = new Mock<IAppDbContext>();
        _aiProviderMock = new Mock<IArchitectureAiProvider>();
        _promptRepoMock = new Mock<IPromptRepository>();
    }

    private static DbSet<T> CreateDbSetMock<T>(List<T> sourceList) where T : class
    {
        var queryable = sourceList.AsQueryable();
        var dbSetMock = new Mock<DbSet<T>>();
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
        
        // This handles async enumerator in a simplified way for in-memory lists during EF standard methods
        dbSetMock.As<IAsyncEnumerable<T>>()
            .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new TestAsyncEnumerator<T>(queryable.GetEnumerator()));
            
        return dbSetMock.Object;
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldGenerateAndSaveArtifact()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var requirementId = Guid.NewGuid();
        var project = new ArchitectureProject("Test Project", "Banking", "Desc", "Owner");
        typeof(ArchitectureProject).GetProperty("Id")!.SetValue(project, projectId);
        
        var req = new RequirementSubmission(projectId, "Req Title", "Req Text", "Domain", new[] { ArchitectureGovernance.Domain.Requirements.ArtifactType.RequirementAnalysis }, "Owner");
        typeof(RequirementSubmission).GetProperty("Id")!.SetValue(req, requirementId);

        var projects = new List<ArchitectureProject> { project };
        var requirements = new List<RequirementSubmission> { req };
        var artifacts = new List<GeneratedArtifact>();

        var projectsDbSet = CreateDbSetMock(projects);
        var reqDbSet = CreateDbSetMock(requirements);
        var artifactsDbSet = CreateDbSetMock(artifacts);

        Mock.Get(artifactsDbSet).Setup(d => d.Add(It.IsAny<GeneratedArtifact>()))
            .Callback<GeneratedArtifact>(a => artifacts.Add(a));

        _contextMock.Setup(c => c.Projects).Returns(projectsDbSet);
        _contextMock.Setup(c => c.Requirements).Returns(reqDbSet);
        _contextMock.Setup(c => c.Artifacts).Returns(artifactsDbSet);

        var promptTemplate = new PromptTemplate("requirement-analysis", "Requirement Analysis", "1.0.0", "Purpose", "Content", "ArtifactType", "Provider");
        _promptRepoMock.Setup(r => r.GetByIdAsync("requirement-analysis", It.IsAny<CancellationToken>()))
            .ReturnsAsync(promptTemplate);

        _aiProviderMock.Setup(a => a.GenerateArtifactDraftAsync(It.IsAny<ArchitectureAiRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ArchitectureAiResponse("RequirementAnalysis", "Markdown Output", "MockProvider", "Name", "1.0", DateTimeOffset.UtcNow, "Success", Array.Empty<string>(), "Review Notice"));

        var handler = new GenerateArtifactCommandHandler(
            _contextMock.Object, _aiProviderMock.Object, _promptRepoMock.Object, NullLogger<GenerateArtifactCommandHandler>.Instance);

        var command = new GenerateArtifactCommand(projectId, requirementId, "RequirementAnalysis");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("RequirementAnalysis", result.ArtifactType);
        Assert.Equal("Markdown Output", result.MarkdownContent);
        Assert.Equal("MockProvider", result.ProviderName);
        
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Single(artifacts);
    }

    [Fact]
    public async Task Handle_GivenInvalidProject_ShouldThrowNotFound()
    {
        // Arrange
        var projects = new List<ArchitectureProject>();
        _contextMock.Setup(c => c.Projects).Returns(CreateDbSetMock(projects));

        var handler = new GenerateArtifactCommandHandler(
            _contextMock.Object, _aiProviderMock.Object, _promptRepoMock.Object, NullLogger<GenerateArtifactCommandHandler>.Instance);
        var command = new GenerateArtifactCommand(Guid.NewGuid(), Guid.NewGuid(), "RequirementAnalysis");

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }
}

// Helper class to support AsyncEnumerator for Mocking EF Core IAsyncEnumerable
internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
{
    private readonly IEnumerator<T> _inner;
    public TestAsyncEnumerator(IEnumerator<T> inner) { _inner = inner; }
    public T Current => _inner.Current;
    public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(_inner.MoveNext());
    public ValueTask DisposeAsync() { _inner.Dispose(); return new ValueTask(); }
}
