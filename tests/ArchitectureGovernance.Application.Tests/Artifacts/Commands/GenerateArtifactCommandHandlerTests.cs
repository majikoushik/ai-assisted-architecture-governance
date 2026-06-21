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

namespace ArchitectureGovernance.Application.Tests.Artifacts.Commands;

public class GenerateArtifactCommandHandlerTests
{
    private readonly AppDbContext _context;
    private readonly Mock<IArchitectureAiProvider> _aiProviderMock;
    private readonly Mock<IPromptRepository> _promptRepoMock;

    public GenerateArtifactCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new AppDbContext(options);
        
        _aiProviderMock = new Mock<IArchitectureAiProvider>();
        _promptRepoMock = new Mock<IPromptRepository>();
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldGenerateAndSaveArtifact()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var requirementId = Guid.NewGuid();
        var project = new ArchitectureProject("Test Project", "Banking", "Desc", "Owner");
        typeof(ArchitectureProject).GetProperty("Id")!.SetValue(project, projectId);
        
        var req = new RequirementSubmission(projectId, "Req Title", "Req Text", "Domain", "Owner", new[] { ArchitectureGovernance.Domain.Requirements.ArtifactType.RequirementAnalysis }, "Domain Context");
        typeof(RequirementSubmission).GetProperty("Id")!.SetValue(req, requirementId);

        _context.Projects.Add(project);
        _context.Requirements.Add(req);
        await _context.SaveChangesAsync();

        var promptTemplate = new PromptTemplate("requirement-analysis", "Requirement Analysis", "1.0.0", "Purpose", "Content", "ArtifactType", "Provider");
        _promptRepoMock.Setup(r => r.GetByIdAsync("requirement-analysis", It.IsAny<CancellationToken>()))
            .ReturnsAsync(promptTemplate);

        _aiProviderMock.Setup(a => a.GenerateArtifactDraftAsync(It.IsAny<ArchitectureAiRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ArchitectureAiResponse("RequirementAnalysis", "Markdown Output", "MockProvider", "Name", "1.0", DateTimeOffset.UtcNow, "Success", Array.Empty<string>(), "Review Notice"));

        var handler = new GenerateArtifactCommandHandler(
            _context, _aiProviderMock.Object, _promptRepoMock.Object, NullLogger<GenerateArtifactCommandHandler>.Instance);

        var command = new GenerateArtifactCommand(projectId, requirementId, "RequirementAnalysis");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("RequirementAnalysis", result.ArtifactType);
        Assert.Equal("Markdown Output", result.MarkdownContent);
        Assert.Equal("MockProvider", result.ProviderName);
        
        var artifacts = await _context.Artifacts.ToListAsync();
        Assert.Single(artifacts);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldGenerateAndSaveHLDArtifact()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var requirementId = Guid.NewGuid();
        var project = new ArchitectureProject("Test Project", "Banking", "Desc", "Owner");
        typeof(ArchitectureProject).GetProperty("Id")!.SetValue(project, projectId);
        
        var req = new RequirementSubmission(projectId, "Req Title", "Req Text", "Domain", "Owner", new[] { ArchitectureGovernance.Domain.Requirements.ArtifactType.HighLevelDesign }, "Domain Context");
        typeof(RequirementSubmission).GetProperty("Id")!.SetValue(req, requirementId);

        _context.Projects.Add(project);
        _context.Requirements.Add(req);
        await _context.SaveChangesAsync();

        var promptTemplate = new PromptTemplate("hld-generation", "HLD Generation", "1.0.0", "Purpose", "Content", "ArtifactType", "Provider");
        _promptRepoMock.Setup(r => r.GetByIdAsync("hld-generation", It.IsAny<CancellationToken>()))
            .ReturnsAsync(promptTemplate);

        _aiProviderMock.Setup(a => a.GenerateArtifactDraftAsync(It.IsAny<ArchitectureAiRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ArchitectureAiResponse("HighLevelDesign", "HLD Markdown Output", "MockProvider", "Name", "1.0", DateTimeOffset.UtcNow, "Success", Array.Empty<string>(), "Review Notice"));

        var handler = new GenerateArtifactCommandHandler(
            _context, _aiProviderMock.Object, _promptRepoMock.Object, NullLogger<GenerateArtifactCommandHandler>.Instance);

        var command = new GenerateArtifactCommand(projectId, requirementId, "HighLevelDesign");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("HighLevelDesign", result.ArtifactType);
        Assert.Equal("HLD Markdown Output", result.MarkdownContent);
        Assert.Equal("MockProvider", result.ProviderName);
        Assert.Equal("Test Project - High-Level Design", result.Title);
        
        var artifacts = await _context.Artifacts.ToListAsync();
        Assert.Single(artifacts);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldGenerateAndSaveLLDArtifact()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var requirementId = Guid.NewGuid();
        var project = new ArchitectureProject("Test Project", "Banking", "Desc", "Owner");
        typeof(ArchitectureProject).GetProperty("Id")!.SetValue(project, projectId);
        
        var req = new RequirementSubmission(projectId, "Req Title", "Req Text", "Domain", "Owner", new[] { ArchitectureGovernance.Domain.Requirements.ArtifactType.LowLevelDesign }, "Domain Context");
        typeof(RequirementSubmission).GetProperty("Id")!.SetValue(req, requirementId);

        _context.Projects.Add(project);
        _context.Requirements.Add(req);
        await _context.SaveChangesAsync();

        var promptTemplate = new PromptTemplate("lld-generation", "LLD Generation", "1.0.0", "Purpose", "Content", "ArtifactType", "Provider");
        _promptRepoMock.Setup(r => r.GetByIdAsync("lld-generation", It.IsAny<CancellationToken>()))
            .ReturnsAsync(promptTemplate);

        _aiProviderMock.Setup(a => a.GenerateArtifactDraftAsync(It.IsAny<ArchitectureAiRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ArchitectureAiResponse("LowLevelDesign", "LLD Markdown Output", "MockProvider", "Name", "1.0", DateTimeOffset.UtcNow, "Success", Array.Empty<string>(), "Review Notice"));

        var handler = new GenerateArtifactCommandHandler(
            _context, _aiProviderMock.Object, _promptRepoMock.Object, NullLogger<GenerateArtifactCommandHandler>.Instance);

        var command = new GenerateArtifactCommand(projectId, requirementId, "LowLevelDesign");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("LowLevelDesign", result.ArtifactType);
        Assert.Equal("LLD Markdown Output", result.MarkdownContent);
        Assert.Equal("MockProvider", result.ProviderName);
        Assert.Equal("Test Project - Low-Level Design", result.Title);
        
        var artifacts = await _context.Artifacts.ToListAsync();
        Assert.Single(artifacts);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldGenerateAndSaveADRArtifact()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var requirementId = Guid.NewGuid();
        var project = new ArchitectureProject("Test Project", "Banking", "Desc", "Owner");
        typeof(ArchitectureProject).GetProperty("Id")!.SetValue(project, projectId);
        
        var req = new RequirementSubmission(projectId, "Req Title", "Req Text", "Domain", "Owner", new[] { ArchitectureGovernance.Domain.Requirements.ArtifactType.ArchitectureDecisionRecord }, "Domain Context");
        typeof(RequirementSubmission).GetProperty("Id")!.SetValue(req, requirementId);

        _context.Projects.Add(project);
        _context.Requirements.Add(req);
        await _context.SaveChangesAsync();

        var promptTemplate = new PromptTemplate("adr-generation", "ADR Generation", "1.0.0", "Purpose", "Content", "ArtifactType", "Provider");
        _promptRepoMock.Setup(r => r.GetByIdAsync("adr-generation", It.IsAny<CancellationToken>()))
            .ReturnsAsync(promptTemplate);

        _aiProviderMock.Setup(a => a.GenerateArtifactDraftAsync(It.IsAny<ArchitectureAiRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ArchitectureAiResponse("ArchitectureDecisionRecord", "ADR Markdown Output", "MockProvider", "Name", "1.0", DateTimeOffset.UtcNow, "Success", Array.Empty<string>(), "Review Notice"));

        var handler = new GenerateArtifactCommandHandler(
            _context, _aiProviderMock.Object, _promptRepoMock.Object, NullLogger<GenerateArtifactCommandHandler>.Instance);

        var command = new GenerateArtifactCommand(projectId, requirementId, "ArchitectureDecisionRecord");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("ArchitectureDecisionRecord", result.ArtifactType);
        Assert.Equal("ADR Markdown Output", result.MarkdownContent);
        Assert.Equal("MockProvider", result.ProviderName);
        Assert.Equal("Test Project - Architecture Decision Record", result.Title);
        
        var artifacts = await _context.Artifacts.ToListAsync();
        Assert.Single(artifacts);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldGenerateAndSaveNFRArtifact()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var requirementId = Guid.NewGuid();
        var project = new ArchitectureProject("Test Project", "Banking", "Desc", "Owner");
        typeof(ArchitectureProject).GetProperty("Id")!.SetValue(project, projectId);
        
        var req = new RequirementSubmission(projectId, "Req Title", "Req Text", "Domain", "Owner", new[] { ArchitectureGovernance.Domain.Requirements.ArtifactType.NonFunctionalRequirementReview }, "Domain Context");
        typeof(RequirementSubmission).GetProperty("Id")!.SetValue(req, requirementId);

        _context.Projects.Add(project);
        _context.Requirements.Add(req);
        await _context.SaveChangesAsync();

        var promptTemplate = new PromptTemplate("nfr-review", "NFR Review", "1.0.0", "Purpose", "Content", "ArtifactType", "Provider");
        _promptRepoMock.Setup(r => r.GetByIdAsync("nfr-review", It.IsAny<CancellationToken>()))
            .ReturnsAsync(promptTemplate);

        _aiProviderMock.Setup(a => a.GenerateArtifactDraftAsync(It.IsAny<ArchitectureAiRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ArchitectureAiResponse("NonFunctionalRequirementReview", "NFR Markdown Output", "MockProvider", "Name", "1.0", DateTimeOffset.UtcNow, "Success", Array.Empty<string>(), "Review Notice"));

        var handler = new GenerateArtifactCommandHandler(
            _context, _aiProviderMock.Object, _promptRepoMock.Object, NullLogger<GenerateArtifactCommandHandler>.Instance);

        var command = new GenerateArtifactCommand(projectId, requirementId, "NonFunctionalRequirementReview");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("NonFunctionalRequirementReview", result.ArtifactType);
        Assert.Equal("NFR Markdown Output", result.MarkdownContent);
        Assert.Equal("MockProvider", result.ProviderName);
        Assert.Equal("Test Project - Non-Functional Requirement Review", result.Title);
        
        var artifacts = await _context.Artifacts.ToListAsync();
        Assert.Single(artifacts);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldGenerateAndSaveSecurityReviewArtifact()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var requirementId = Guid.NewGuid();
        var project = new ArchitectureProject("Test Project", "Banking", "Desc", "Owner");
        typeof(ArchitectureProject).GetProperty("Id")!.SetValue(project, projectId);
        
        var req = new RequirementSubmission(projectId, "Req Title", "Req Text", "Domain", "Owner", new[] { ArchitectureGovernance.Domain.Requirements.ArtifactType.SecurityReview }, "Domain Context");
        typeof(RequirementSubmission).GetProperty("Id")!.SetValue(req, requirementId);

        _context.Projects.Add(project);
        _context.Requirements.Add(req);
        await _context.SaveChangesAsync();

        var promptTemplate = new PromptTemplate("security-review", "Security Review", "1.0.0", "Purpose", "Content", "ArtifactType", "Provider");
        _promptRepoMock.Setup(r => r.GetByIdAsync("security-review", It.IsAny<CancellationToken>()))
            .ReturnsAsync(promptTemplate);

        _aiProviderMock.Setup(a => a.GenerateArtifactDraftAsync(It.IsAny<ArchitectureAiRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ArchitectureAiResponse("SecurityReview", "Security Markdown Output", "MockProvider", "Name", "1.0", DateTimeOffset.UtcNow, "Success", Array.Empty<string>(), "Review Notice"));

        var handler = new GenerateArtifactCommandHandler(
            _context, _aiProviderMock.Object, _promptRepoMock.Object, NullLogger<GenerateArtifactCommandHandler>.Instance);

        var command = new GenerateArtifactCommand(projectId, requirementId, "SecurityReview");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("SecurityReview", result.ArtifactType);
        Assert.Equal("Security Markdown Output", result.MarkdownContent);
        Assert.Equal("MockProvider", result.ProviderName);
        Assert.Equal("Test Project - Security Review", result.Title);
        
        var artifacts = await _context.Artifacts.ToListAsync();
        Assert.Single(artifacts);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldGenerateAndSaveApiContractReviewArtifact()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var requirementId = Guid.NewGuid();
        var project = new ArchitectureProject("Test Project", "Banking", "Desc", "Owner");
        typeof(ArchitectureProject).GetProperty("Id")!.SetValue(project, projectId);
        
        var req = new RequirementSubmission(projectId, "Req Title", "Req Text", "Domain", "Owner", new[] { ArchitectureGovernance.Domain.Requirements.ArtifactType.ApiContractReview }, "Domain Context");
        typeof(RequirementSubmission).GetProperty("Id")!.SetValue(req, requirementId);

        _context.Projects.Add(project);
        _context.Requirements.Add(req);
        await _context.SaveChangesAsync();

        var promptTemplate = new PromptTemplate("api-contract-review", "API Contract Review", "1.0.0", "Purpose", "Content", "ArtifactType", "Provider");
        _promptRepoMock.Setup(r => r.GetByIdAsync("api-contract-review", It.IsAny<CancellationToken>()))
            .ReturnsAsync(promptTemplate);

        _aiProviderMock.Setup(a => a.GenerateArtifactDraftAsync(It.IsAny<ArchitectureAiRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ArchitectureAiResponse("ApiContractReview", "API Markdown Output", "MockProvider", "Name", "1.0", DateTimeOffset.UtcNow, "Success", Array.Empty<string>(), "Review Notice"));

        var handler = new GenerateArtifactCommandHandler(
            _context, _aiProviderMock.Object, _promptRepoMock.Object, NullLogger<GenerateArtifactCommandHandler>.Instance);

        var command = new GenerateArtifactCommand(projectId, requirementId, "ApiContractReview");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("ApiContractReview", result.ArtifactType);
        Assert.Equal("API Markdown Output", result.MarkdownContent);
        Assert.Equal("MockProvider", result.ProviderName);
        Assert.Equal("Test Project - API Contract Review", result.Title);
        
        var artifacts = await _context.Artifacts.ToListAsync();
        Assert.Single(artifacts);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldGenerateAndSaveRiskAndAssumptionReviewArtifact()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var requirementId = Guid.NewGuid();
        var project = new ArchitectureProject("Test Project", "Banking", "Desc", "Owner");
        typeof(ArchitectureProject).GetProperty("Id")!.SetValue(project, projectId);
        
        var req = new RequirementSubmission(projectId, "Req Title", "Req Text", "Domain", "Owner", new[] { ArchitectureGovernance.Domain.Requirements.ArtifactType.RiskAndAssumptionReview }, "Domain Context");
        typeof(RequirementSubmission).GetProperty("Id")!.SetValue(req, requirementId);

        _context.Projects.Add(project);
        _context.Requirements.Add(req);
        await _context.SaveChangesAsync();

        var promptTemplate = new PromptTemplate("risk-assumption-review", "Risk Review", "1.0.0", "Purpose", "Content", "ArtifactType", "Provider");
        _promptRepoMock.Setup(r => r.GetByIdAsync("risk-assumption-review", It.IsAny<CancellationToken>()))
            .ReturnsAsync(promptTemplate);

        _aiProviderMock.Setup(a => a.GenerateArtifactDraftAsync(It.IsAny<ArchitectureAiRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ArchitectureAiResponse("RiskAndAssumptionReview", "Risk Markdown Output", "MockProvider", "Name", "1.0", DateTimeOffset.UtcNow, "Success", Array.Empty<string>(), "Review Notice"));

        var handler = new GenerateArtifactCommandHandler(
            _context, _aiProviderMock.Object, _promptRepoMock.Object, NullLogger<GenerateArtifactCommandHandler>.Instance);

        var command = new GenerateArtifactCommand(projectId, requirementId, "RiskAndAssumptionReview");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("RiskAndAssumptionReview", result.ArtifactType);
        Assert.Equal("Risk Markdown Output", result.MarkdownContent);
        Assert.Equal("MockProvider", result.ProviderName);
        Assert.Equal("Test Project - Risk and Assumption Review", result.Title);
        
        var artifacts = await _context.Artifacts.ToListAsync();
        Assert.Single(artifacts);
    }

    [Fact]
    public async Task Handle_GivenInvalidProject_ShouldThrowNotFound()
    {
        // Arrange
        var handler = new GenerateArtifactCommandHandler(
            _context, _aiProviderMock.Object, _promptRepoMock.Object, NullLogger<GenerateArtifactCommandHandler>.Instance);
        var command = new GenerateArtifactCommand(Guid.NewGuid(), Guid.NewGuid(), "RequirementAnalysis");

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }
}
