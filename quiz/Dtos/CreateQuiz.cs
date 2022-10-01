namespace quiz.Dtos;
public class CreateQuiz
{
    public CreateQuiz(string? title, string? description, DateTimeOffset startTime, DateTimeOffset endTime, string? password)
    {
        Title = title;
        Description = description;
        StartTime = startTime;
        EndTime = endTime;
        Password = password;
    }

    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public string? Password { get; set; }
}