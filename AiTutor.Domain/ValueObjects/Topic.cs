namespace AiTutor.Domain.ValueObjects;

public class Topic
{
    public string Name { get; }

    public Topic(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Topic name cannot be null or empty.");

        Name = name;
    }
}