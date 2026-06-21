using ArchitectureGovernance.Domain.Prompts;

namespace ArchitectureGovernance.Application.Prompts.Services;

public interface IPromptRepository
{
    Task<IEnumerable<PromptTemplate>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PromptTemplate?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
}
