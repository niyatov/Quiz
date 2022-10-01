namespace quiz.Dtos.Question;

public class Question
{
    public ulong Id { get; set; }
    public ulong TopicId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }
    public int TimeAllowed { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}