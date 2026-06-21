using ArchitectureGovernance.Application.Prompts.DTOs;
using ArchitectureGovernance.Application.Prompts.Services;
using MediatR;

namespace ArchitectureGovernance.Application.Prompts.Queries;

public record GetPromptTemplatesQuery() : IRequest<IEnumerable<PromptTemplateDto>>;

public sealed class GetPromptTemplatesQueryHandler(IPromptRepository promptRepository) 
    : IRequestHandler<GetPromptTemplatesQuery, IEnumerable<PromptTemplateDto>>
{
    public async Task<IEnumerable<PromptTemplateDto>> Handle(GetPromptTemplatesQuery request, CancellationToken cancellationToken)
    {
        var templates = await promptRepository.GetAllAsync(cancellationToken);
        
        return templates.Select(t => new PromptTemplateDto(
            t.Id,
            t.Name,
            t.ArtifactType,
            t.Version,
            t.Purpose,
            t.Content,
            t.Status));
    }
}
