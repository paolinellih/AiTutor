namespace AiTutor.Domain.Entities;

public class AiResponse(string prompt, string responseText)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Prompt { get; private set; } = prompt ?? throw new ArgumentNullException(nameof(prompt));
    public string Response { get; private set; } = responseText ?? throw new ArgumentNullException(nameof(responseText));
    public DateTime GeneratedAt { get; private set; } = DateTime.UtcNow;
}
