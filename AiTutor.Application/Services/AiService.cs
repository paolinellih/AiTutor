using AiTutor.Domain.Entities;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration; 

namespace AiTutor.Application.Services;

public class AiService
{
    private readonly IAiClient _aiClient;
    private readonly IConnectionMultiplexer _redis;
    
    public AiService(IAiClient aiClient, IConfiguration configuration)
    {
        _aiClient = aiClient;
        var redisConnectionString = configuration["REDIS_CONNECTION_STRING"];
        _redis = ConnectionMultiplexer.Connect(redisConnectionString);
    }
    public async Task<AiResponse> GetAiResponseAsync(string prompt)
    {
        // Step 1: Extract topic from the prompt
        var topic = await ExtractTopicFromPromptAsync(prompt);
        if (string.IsNullOrEmpty(topic))
        {
            return new AiResponse(prompt, "Sorry, I couldn't identify the topic from your request. Please specify a topic.");
        }

        // Step 2: Check if the topic is allowed (from Redis)
        var allowedTopics = await _redis.GetDatabase().ListRangeAsync("allowed_topics:default");
        if (!allowedTopics.Contains(topic))
        {
            return new AiResponse(prompt, "Sorry, the topic you're asking about is not part of the allowed curriculum.");
        }

        // Step 3: Check if the prompt contains sensitive words
        var sensitiveTopics = await _redis.GetDatabase().ListRangeAsync("sensitive_topics");
        if (sensitiveTopics.Any(sensitiveWord => prompt.Contains(sensitiveWord, StringComparison.OrdinalIgnoreCase)))
        {
            return new AiResponse(prompt, "Sorry, the topic you're asking about is sensitive. Let's stay focused on your lesson.");
        }

        // Step 4: If all checks pass, send the prompt to Ollama AI
        var context = $"Explain '{prompt}' in detail.";
        
        var aiResponseText = await _aiClient.GetResponseAsync(context);
        
        return new AiResponse(prompt, aiResponseText);
    }
    
    private async Task<string> ExtractTopicFromPromptAsync(string prompt)
    {
        var db = _redis.GetDatabase();
    
        // Retrieve the list of allowed topics from Redis
        var allowedTopics = await db.ListRangeAsync("allowed_topics:default");

        // Check if any of the allowed topics are mentioned in the prompt
        foreach (var allowedTopic in allowedTopics)
        {
            if (prompt.Contains(allowedTopic, StringComparison.OrdinalIgnoreCase))
            {
                return allowedTopic; // Found a topic in the prompt
            }
        }

        return null; // No topic found
    }

}