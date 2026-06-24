using ArchitectureGovernance.AI.Abstractions;
using Xunit;
using ArchitectureGovernance.Application.Artifacts.Commands;
using ArchitectureGovernance.Application.Prompts.Services;
using ArchitectureGovernance.Domain.Artifacts;
using ArchitectureGovernance.Domain.Projects;
using ArchitectureGovernance.Domain.Prompts;
using ArchitectureGovernance.Domain.Requirements;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using ArchitectureGovernance.Infrastructure.Persistence;
using ArchitectureGovernance.Application.Common.Interfaces;

namespace ArchitectureGovernance.Application.Tests.Artifacts.Commands;

public class GenerateArtifactCommandHandlerTests
{
    private readonly AppDbContext _context;
    private readonly Mock<IArchitectureAiProvider> _aiProviderMock;
    private readonly Mock<IPromptRepository> _promptRepoMock;
    private readonly Mock<ICorrelationIdProvider> _correlationIdProviderMock;

    public GenerateArtifactCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new AppDbContext(options);
        
        _aiProviderMock = new Mock<IArchitectureAiProvider>();
        _promptRepoMock = new Mock<IPromptRepository>();
        _correlationIdProviderMock = new Mock<ICorrelationIdProvider>();
        _correlationIdProviderMock.Setup(c => c.CorrelationId).Returns("test-correlation-id");
    }

    [Theory]
    [InlineData(ArchitectureGovernance.Domain.ArtifactType.RequirementAnalysis, "requirement-analysis", "Requirement Analysis", "Requirement Analysis", "Test Project - Requirement Analysis")]
    [InlineData(ArchitectureGovernance.Domain.ArtifactType.HighLevelDesign, "hld-generation", "HighLevelDesign", "HLD Markdown Output", "Test Project - HighLevelDesign")]
    [InlineData(ArchitectureGovernance.Domain.ArtifactType.LowLevelDesign, "lld-generation", "LowLevelDesign", "LLD Markdown Output", "Test Project - LowLevelDesign")]
    [InlineData(ArchitectureGovernance.Domain.ArtifactType.ArchitectureDecisionRecord, "adr-generation", "ArchitectureDecisionRecord", "ADR Markdown Output", "Test Project - ArchitectureDecisionRecord")]
    [InlineData(ArchitectureGovernance.Domain.ArtifactType.NonFunctionalRequirementReview, "nfr-review", "NonFunctionalRequirementReview", "NFR Markdown Output", "Test Project - NonFunctionalRequirementReview")]
    [InlineData(ArchitectureGovernance.Domain.ArtifactType.ApiContractReview, "api-contract-review", "ApiContractReview", "API Markdown Output", "Test Project - ApiContractReview")]
    [InlineData(ArchitectureGovernance.Domain.ArtifactType.SecurityReview, "security-review", "SecurityReview", "Security Markdown Output", "Test Project - SecurityReview")]
    [InlineData(ArchitectureGovernance.Domain.ArtifactType.RiskAndAssumptionReview, "risk-assumption-review", "RiskAndAssumptionReview", "Risk Markdown Output", "Test Project - RiskAndAssumptionReview")]
    public async Task Handle_GivenValidRequest_ShouldGenerateAndSaveArtifact(
        ArchitectureGovernance.Domain.ArtifactType artifactType,
        string promptId,
        string templateName,
        string markdownOutput,
        string expectedTitle)
    {
        // Arrange
        var project = new ArchitectureProject("Test Project", "Banking", "Desc", "Owner");
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        var projectId = project.Id;
        
        // We cheat the Enum parsing for requirements artifact type mapping
        Enum.TryParse<ArchitectureGovernance.Domain.Requirements.ArtifactType>(artifactType.ToString(), out var reqArtifactType);
        var req = new RequirementSubmission(projectId, "Req Title", "Req Text", "Domain", "Owner", new[] { reqArtifactType }, "Domain Context");
        
        _context.Requirements.Add(req);
        await _context.SaveChangesAsync();
        var requirementId = req.Id;

        var promptTemplate = new PromptTemplate(promptId, templateName, "1.0.0", "Purpose", "Content", artifactType.ToString(), "Provider");
        _promptRepoMock.Setup(r => r.GetByIdAsync(promptId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(promptTemplate);

        _aiProviderMock.Setup(a => a.GenerateArtifactDraftAsync(It.IsAny<ArchitectureAiRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ArchitectureAiResponse(artifactType.ToString(), markdownOutput, "MockProvider", templateName, "1.0", DateTimeOffset.UtcNow, "Success", Array.Empty<string>(), "Review Notice"));

        var handler = new GenerateArtifactCommandHandler(
            _context, _aiProviderMock.Object, _promptRepoMock.Object, NullLogger<GenerateArtifactCommandHandler>.Instance, _correlationIdProviderMock.Object);

        var command = new GenerateArtifactCommand(projectId, requirementId, artifactType);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(artifactType.ToString(), result.ArtifactType);
        Assert.Equal(markdownOutput, result.MarkdownContent);
        Assert.Equal("MockProvider", result.ProviderName);
        Assert.Equal(expectedTitle, result.Title);
        
        var artifacts = await _context.Artifacts.ToListAsync();
        Assert.Single(artifacts);
        Assert.Equal("v1.0.1", artifacts[0].Version);
        
        var logs = await _context.AIInteractionLogs.ToListAsync();
        Assert.Single(logs);
        Assert.Equal("Success", logs[0].Status);
    }

    [Fact]
    public async Task Handle_GivenInvalidProject_ShouldThrowNotFound()
    {
        // Arrange
        var handler = new GenerateArtifactCommandHandler(
            _context, _aiProviderMock.Object, _promptRepoMock.Object, NullLogger<GenerateArtifactCommandHandler>.Instance, _correlationIdProviderMock.Object);
        var command = new GenerateArtifactCommand(Guid.NewGuid(), Guid.NewGuid(), ArchitectureGovernance.Domain.ArtifactType.RequirementAnalysis);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }
}
