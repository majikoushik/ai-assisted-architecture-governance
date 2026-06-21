namespace ArchitectureGovernance.Api.Tests;

using Xunit;

public class ApiAssemblyTests
{
    [Fact]
    public void ProgramTypeIsAvailableForIntegrationTesting()
    {
        var assemblyName = typeof(Program).Assembly.GetName().Name;

        Assert.Equal("ArchitectureGovernance.Api", assemblyName);
    }
}
