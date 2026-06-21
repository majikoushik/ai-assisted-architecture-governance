using ArchitectureGovernance.Domain;
using Xunit;

namespace ArchitectureGovernance.Domain.Tests;

public sealed class ReviewStatusTests
{
    [Fact]
    public void ReviewStatus_IncludesHumanApprovalStates()
    {
        Assert.Contains(ReviewStatus.NeedsReview, Enum.GetValues<ReviewStatus>());
        Assert.Contains(ReviewStatus.Approved, Enum.GetValues<ReviewStatus>());
    }
}
