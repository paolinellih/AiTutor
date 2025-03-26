using System.Text.Json;
using AiTutor.Domain.ValueObjects;
using StackExchange.Redis;

namespace AiTutor.Infrastructure.Seeders;

public static class RedisSeeder
{
    public static void SeedData(IConnectionMultiplexer connectionMultiplexer)
    {
        var db = connectionMultiplexer.GetDatabase();

        // Seed Sensitive Topics
        var sensitiveTopics = new List<string> { "reproduction", "sex", "violence", "drugs" };
        db.ListRightPush("sensitive_topics", sensitiveTopics.Select(x => (RedisValue)x).ToArray());

        // Seed Allowed Topics List (no grades involved)
        var allowedTopics = new List<string>
        {
            "Physics",
            "Mathematics",
            "Chemistry",
            "History",
            "Biology",
            "Literature",
            "Geography"
        };

        // Store the allowed topics in Redis
        db.ListRightPush("allowed_topics:default", allowedTopics.Select(x => (RedisValue)x).ToArray());

        // Optionally, store the allowed topics in a combined list (for easy checking across topics)
        db.ListRightPush("known_topics", allowedTopics.Select(x => (RedisValue)x).ToArray());
    }
}