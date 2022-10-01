namespace quiz.Dtos;
public class Quiz
{
    public Quiz(ulong id, string? title, string? description, DateTimeOffset startTime, DateTimeOffset endTime, DateTimeOffset createdAt, DateTimeOffset updatedAt)
    {
        Id = id;
        Title = title;
        Description = description;
        StartTime = startTime;
        EndTime = endTime;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public ulong Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}