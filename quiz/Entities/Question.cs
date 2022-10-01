namespace quiz.Entities;
public class Question : EntityBase
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public EQuestionType Type { get; set; }
    public int TimeAllowed { get; set; }
    public ulong TopicId { get; set; }
    public virtual Topic? Topic { get; set; }

    [Obsolete("Used only for entity binding.")]
    public Question() { }
    public Question( ulong topicId,string? title, string? description, EQuestionType type, int timeAllowed)
    {
        TopicId = topicId;
        Title = title;
        Description = description;
        Type = type;
        TimeAllowed = timeAllowed;
    }
}