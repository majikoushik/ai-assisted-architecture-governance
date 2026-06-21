using SystemClientModel = System.ClientModel;
using ArchitectureGovernance.AI.Abstractions;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenAI.Chat;

namespace ArchitectureGovernance.AI.AzureOpenAI;

public sealed class AzureOpenAiProvider : IArchitectureAiProvider
{
    private readonly AzureOpenAiOptions _options;
    private readonly ILogger<AzureOpenAiProvider> _logger;

    public AzureOpenAiProvider(IOptions<AzureOpenAiOptions> options, ILogger<AzureOpenAiProvider> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public async Task<ArchitectureAiResponse> GenerateArtifactDraftAsync(
        ArchitectureAiRequest request,
        CancellationToken cancellationToken = default)
    {
        var humanReviewNotice = "This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.";

        try
        {
            if (string.IsNullOrWhiteSpace(_options.Endpoint) || string.IsNullOrWhiteSpace(_options.ApiKey) || string.IsNullOrWhiteSpace(_options.DeploymentName))
            {
                _logger.LogWarning("Azure OpenAI configuration is missing. Falling back to an error response.");
                return new ArchitectureAiResponse(
                    ArtifactType: request.ArtifactType,
                    Markdown: $"> {humanReviewNotice}\n\n# Error: Azure OpenAI Configuration Missing\n\nPlease configure the Azure OpenAI settings in your local secrets or environment variables.",
                    ProviderName: "AzureOpenAI (Misconfigured)",
                    PromptTemplateName: request.PromptTemplateName,
                    PromptTemplateVersion: request.PromptTemplateVersion,
                    GenerationTimestamp: DateTimeOffset.UtcNow,
                    Status: "Failed",
                    Warnings: new[] { "Missing Azure OpenAI configuration" },
                    HumanReviewNotice: humanReviewNotice);
            }

            var client = new AzureOpenAIClient(new Uri(_options.Endpoint), new SystemClientModel.ApiKeyCredential(_options.ApiKey));
            var chatClient = client.GetChatClient(_options.DeploymentName);

            var systemPrompt = request.PromptTemplateContent;
            
            var userPrompt = $@"
Requirement Title: {request.RequirementTitle}
Business Domain: {request.BusinessDomain}
Domain Context: {request.DomainContext}

Requirement Text:
{request.RequirementText}
";

            var messages = new List<ChatMessage>
            {
                ChatMessage.CreateSystemMessage(systemPrompt),
                ChatMessage.CreateUserMessage(userPrompt)
            };

            var chatOptions = new ChatCompletionOptions
            {
                Temperature = 0.3f, // Keeping it somewhat deterministic and professional
                MaxOutputTokenCount = 4000
            };

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(TimeSpan.FromSeconds(_options.TimeoutSeconds));

            _logger.LogInformation("Calling Azure OpenAI. CorrelationId: {CorrelationId}, Deployment: {DeploymentName}", request.CorrelationId, _options.DeploymentName);

            var sw = System.Diagnostics.Stopwatch.StartNew();
            var completionResult = await chatClient.CompleteChatAsync(messages, chatOptions, cts.Token);
            sw.Stop();

            var markdown = completionResult.Value.Content[0].Text;

            // Ensure the human review notice is at the top
            if (!markdown.Contains(humanReviewNotice))
            {
                markdown = $"> {humanReviewNotice}\n\n" + markdown;
            }

            _logger.LogInformation(
                "AI Telemetry - Provider: {ProviderName}, Type: {ArtifactType}, Template: {PromptTemplateName} v{PromptTemplateVersion}, DurationMs: {DurationMs}, Status: {Status}, CorrelationId: {CorrelationId}",
                "AzureOpenAI",
                request.ArtifactType,
                request.PromptTemplateName,
                request.PromptTemplateVersion,
                sw.ElapsedMilliseconds,
                "Success",
                request.CorrelationId);

            return new ArchitectureAiResponse(
                ArtifactType: request.ArtifactType,
                Markdown: markdown,
                ProviderName: "AzureOpenAI",
                PromptTemplateName: request.PromptTemplateName,
                PromptTemplateVersion: request.PromptTemplateVersion,
                GenerationTimestamp: DateTimeOffset.UtcNow,
                Status: "Success",
                Warnings: Array.Empty<string>(),
                HumanReviewNotice: humanReviewNotice);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to call Azure OpenAI provider. CorrelationId: {CorrelationId}", request.CorrelationId);
            
            _logger.LogInformation(
                "AI Telemetry - Provider: {ProviderName}, Type: {ArtifactType}, Template: {PromptTemplateName} v{PromptTemplateVersion}, DurationMs: {DurationMs}, Status: {Status}, CorrelationId: {CorrelationId}",
                "AzureOpenAI",
                request.ArtifactType,
                request.PromptTemplateName,
                request.PromptTemplateVersion,
                -1,
                "Failed",
                request.CorrelationId);

            return new ArchitectureAiResponse(
                ArtifactType: request.ArtifactType,
                Markdown: $"> {humanReviewNotice}\n\n# Error: AI Generation Failed\n\nThere was an error generating the artifact using Azure OpenAI.",
                ProviderName: "AzureOpenAI",
                PromptTemplateName: request.PromptTemplateName,
                PromptTemplateVersion: request.PromptTemplateVersion,
                GenerationTimestamp: DateTimeOffset.UtcNow,
                Status: "Failed",
                Warnings: new[] { ex.Message },
                HumanReviewNotice: humanReviewNotice);
        }
    }
}
