using ArchitectureGovernance.Application.AIInteractions.DTOs;
using ArchitectureGovernance.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureGovernance.Application.AIInteractions.Queries;

public record GetAIInteractionByIdQuery(Guid Id) : IRequest<AIInteractionDto?>;

public class GetAIInteractionByIdQueryHandler : IRequestHandler<GetAIInteractionByIdQuery, AIInteractionDto?>
{
    private readonly IAppDbContext _context;

    public GetAIInteractionByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<AIInteractionDto?> Handle(GetAIInteractionByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.AIInteractionLogs
            .Where(x => x.Id == request.Id)
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
            .FirstOrDefaultAsync(cancellationToken);
    }
}
