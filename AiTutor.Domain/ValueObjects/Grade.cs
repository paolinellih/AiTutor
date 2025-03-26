namespace AiTutor.Domain.ValueObjects;

public class Grade
{
    public string Subject { get; }
    public string Value { get; }

    public Grade(string subject, string value)
    {
        if (string.IsNullOrEmpty(subject)) throw new ArgumentException("Subject cannot be null or empty.");
        if (string.IsNullOrEmpty(value)) throw new ArgumentException("Grade value cannot be null or empty.");

        Subject = subject;
        Value = value;
    }
        
    public override bool Equals(object obj)
    {
        if (obj is Grade other)
        {
            return Subject == other.Subject && Value == other.Value;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Subject, Value);
    }
}