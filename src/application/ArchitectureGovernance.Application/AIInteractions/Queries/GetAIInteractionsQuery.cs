using ArchitectureGovernance.Application.AIInteractions.DTOs;
using ArchitectureGovernance.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureGovernance.Application.AIInteractions.Queries;

public record GetAIInteractionsQuery : IRequest<List<AIInteractionDto>>;

public class GetAIInteractionsQueryHandler : IRequestHandler<GetAIInteractionsQuery, List<AIInteractionDto>>
{
    private readonly IAppDbContext _context;

    public GetAIInteractionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<AIInteractionDto>> Handle(GetAIInteractionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.AIInteractionLogs
            .OrderByDescending(x => x.RequestTimestamp)
            .Select(x => new AIInteractionDto(
                x.Id,
                x.ProjectId,
                x.RequirementSubmissionId,
                x.ProviderName,
                x.ModelName,
                x.PromptTemplateVersion,
                x.RequestTimestamp,
                x.ResponseTimestamp,
                x.Status,
                x.TokenCountEstimate,
                x.ErrorSummary,
                x.CorrelationId))
            .ToListAsync(cancellationToken);
    }
}
