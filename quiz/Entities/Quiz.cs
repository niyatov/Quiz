namespace quiz.Entities;
public class Quiz : EntityBase
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public string? PasswordHash { get; set; } 
}