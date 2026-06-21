using ArchitectureGovernance.Domain.Projects;

namespace ArchitectureGovernance.Domain.Requirements;

public class RequirementSubmission
{
    public Guid Id { get; private set; }
    public Guid ProjectId { get; private set; }
    public string Title { get; private set; }
    public string RequirementText { get; private set; }
    public string BusinessDomain { get; private set; }
    public string? DomainContext { get; private set; }
    public string SubmittedBy { get; private set; }
    public RequirementStatus Status { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    // Represented as a JSON array or comma-separated string based on EF Core config.
    // We will use EF Core primitive collections (built-in JSON support) for string[]
    private readonly List<ArtifactType> _expectedArtifactTypes = new();
    public IReadOnlyCollection<ArtifactType> ExpectedArtifactTypes => _expectedArtifactTypes.AsReadOnly();

    public ArchitectureProject Project { get; private set; } = null!;

#pragma warning disable CS8618 // Required by EF Core
    private RequirementSubmission() { }
#pragma warning restore CS8618

    public RequirementSubmission(
        Guid projectId,
        string title,
        string requirementText,
        string businessDomain,
        string submittedBy,
        IEnumerable<ArtifactType> expectedArtifactTypes,
        string? domainContext = null)
    {
        Id = Guid.NewGuid();
        ProjectId = projectId;
        Title = title;
        RequirementText = requirementText;
        BusinessDomain = businessDomain;
        SubmittedBy = submittedBy;
        DomainContext = domainContext;
        
        _expectedArtifactTypes.AddRange(expectedArtifactTypes);

        Status = RequirementStatus.Draft;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void UpdateDetails(string title, string requirementText, string businessDomain, string? domainContext, IEnumerable<ArtifactType> expectedArtifactTypes)
    {
        Title = title;
        RequirementText = requirementText;
        BusinessDomain = businessDomain;
        DomainContext = domainContext;
        
        _expectedArtifactTypes.Clear();
        _expectedArtifactTypes.AddRange(expectedArtifactTypes);

        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void UpdateStatus(RequirementStatus status)
    {
        Status = status;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
