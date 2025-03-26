namespace AiTutor.Application;

public interface IAiClient
{
    Task<string> GetResponseAsync(string prompt);
}