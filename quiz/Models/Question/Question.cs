namespace quiz.Models.Question;
public class Question 
{
    public ulong Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public EQuestionType Type { get; set; }
    public int TimeAllowed { get; set; }
    public ulong TopicId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    
    
}