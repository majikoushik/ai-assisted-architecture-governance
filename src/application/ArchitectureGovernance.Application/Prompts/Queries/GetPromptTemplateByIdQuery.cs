using ArchitectureGovernance.Application.Prompts.DTOs;
using ArchitectureGovernance.Application.Prompts.Services;
using MediatR;

namespace ArchitectureGovernance.Application.Prompts.Queries;

public record GetPromptTemplateByIdQuery(string Id) : IRequest<PromptTemplateDto?>;

public sealed class GetPromptTemplateByIdQueryHandler(IPromptRepository promptRepository) 
    : IRequestHandler<GetPromptTemplateByIdQuery, PromptTemplateDto?>
{
    public async Task<PromptTemplateDto?> Handle(GetPromptTemplateByIdQuery request, CancellationToken cancellationToken)
    {
        var template = await promptRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (template is null)
        {
            return null;
        }

        return new PromptTemplateDto(
            template.Id,
            template.Name,
            template.ArtifactType,
            template.Version,
            template.Purpose,
            template.Content,
            template.Status);
    }
}
