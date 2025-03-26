namespace AiTutor.Domain.ValueObjects;

public class Lesson
{
    public string Topic { get; }
    public TimeSpan Duration { get; }

    public Lesson(string topic, TimeSpan duration)
    {
        if (string.IsNullOrEmpty(topic)) throw new ArgumentException("Topic cannot be null or empty.");

        Topic = topic;
        Duration = duration;
    }
}