using System.Text.RegularExpressions;
using ArchitectureGovernance.Application.Prompts.Services;
using ArchitectureGovernance.Domain.Prompts;
using ArchitectureGovernance.Domain.Requirements;

namespace ArchitectureGovernance.Infrastructure.Filesystem;

public sealed class FilePromptRepository : IPromptRepository
{
    private readonly string _promptsDirectory;

    public FilePromptRepository()
    {
        // In a real application, this path should be configurable.
        // Assuming the repository root contains the "prompts" folder and the API runs from its bin/Debug/net8.0 or similar.
        var baseDir = AppContext.BaseDirectory;
        var srcIndex = baseDir.IndexOf("src", StringComparison.OrdinalIgnoreCase);
        
        if (srcIndex >= 0)
        {
            _promptsDirectory = Path.Combine(baseDir.Substring(0, srcIndex), "prompts");
        }
        else
        {
            // Fallback for execution from the root (e.g. dotnet run from src/api/...)
            _promptsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "prompts");
            if (!Directory.Exists(_promptsDirectory))
            {
                 _promptsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "prompts");
            }
        }
    }

    public async Task<IEnumerable<PromptTemplate>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        if (!Directory.Exists(_promptsDirectory))
        {
            return Array.Empty<PromptTemplate>();
        }

        var files = Directory.GetFiles(_promptsDirectory, "*.md");
        var templates = new List<PromptTemplate>();

        foreach (var file in files)
        {
            var fileName = Path.GetFileNameWithoutExtension(file);
            if (fileName.Equals("README", StringComparison.OrdinalIgnoreCase)) continue;

            templates.Add(await ParsePromptFileAsync(file, fileName, cancellationToken));
        }

        return templates;
    }

    public async Task<PromptTemplate?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(_promptsDirectory, $"{id}.md");
        if (!File.Exists(filePath))
        {
            return null;
        }

        return await ParsePromptFileAsync(filePath, id, cancellationToken);
    }

    private async Task<PromptTemplate> ParsePromptFileAsync(string filePath, string id, CancellationToken cancellationToken)
    {
        var content = await File.ReadAllTextAsync(filePath, cancellationToken);

        var nameMatch = Regex.Match(content, @"^#\s+(.+)$", RegexOptions.Multiline);
        var name = nameMatch.Success ? nameMatch.Groups[1].Value.Trim() : id;

        var versionMatch = Regex.Match(content, @"Version:\s*`([^`]+)`");
        var version = versionMatch.Success ? versionMatch.Groups[1].Value.Trim() : "v1.0.0";

        var purposeMatch = Regex.Match(content, @"## Purpose\s*\n+(.*?)(?=\n##|\z)", RegexOptions.Singleline);
        var purpose = purposeMatch.Success ? purposeMatch.Groups[1].Value.Trim() : "No purpose provided.";

        var artifactType = MapFilenameToArtifactType(id);

        return new PromptTemplate(
            Id: id,
            Name: name,
            ArtifactType: artifactType,
            Version: version,
            Purpose: purpose,
            Content: content,
            Status: "Active"
        );
    }

    private string MapFilenameToArtifactType(string filename)
    {
        return filename.ToLowerInvariant() switch
        {
            "requirement-analysis" => ArtifactType.RequirementAnalysis.ToString(),
            "hld-generation" => ArtifactType.HighLevelDesign.ToString(),
            "lld-generation" => ArtifactType.LowLevelDesign.ToString(),
            "adr-generation" => ArtifactType.ArchitectureDecisionRecord.ToString(),
            "nfr-review" => ArtifactType.NonFunctionalRequirementReview.ToString(),
            "api-contract-review" => ArtifactType.ApiContractReview.ToString(),
            "security-review" => ArtifactType.SecurityReview.ToString(),
            "risk-assumption-review" => ArtifactType.RiskAndAssumptionReview.ToString(),
            _ => filename
        };
    }
}
