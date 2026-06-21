using SharedKernel;

namespace ArchitectureGovernance.Domain.Projects;

public class ArchitectureProject : Entity
{
    public string Name { get; private set; }
    public string BusinessDomain { get; private set; }
    public string Description { get; private set; }
    public string Owner { get; private set; }
    public ProjectStatus Status { get; private set; }

#pragma warning disable CS8618
    private ArchitectureProject()
    {
        // Required for EF Core
    }
#pragma warning restore CS8618

    public ArchitectureProject(string name, string businessDomain, string description, string owner)
    {
        Name = name;
        BusinessDomain = businessDomain;
        Description = description;
        Owner = owner;
        Status = ProjectStatus.Draft;
    }

    public void UpdateDetails(string name, string businessDomain, string description, string owner)
    {
        Name = name;
        BusinessDomain = businessDomain;
        Description = description;
        Owner = owner;
        
        // Entity abstract class doesn't have an Update method that automatically handles UpdatedAt.
        // But we have `public DateTimeOffset? UpdatedAt { get; protected set; }` in Entity.
        // We'll update it directly here.
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void UpdateStatus(ProjectStatus status)
    {
        Status = status;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
