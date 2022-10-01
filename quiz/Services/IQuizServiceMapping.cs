using quiz.Models.Quiz;

namespace quiz.Services;
public partial class QuizService
{
    private static Quiz ToModel(Entities.Quiz entity)
    {
        return new Quiz()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
        };
    }
}