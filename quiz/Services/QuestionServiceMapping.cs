using System.Linq.Expressions;
using quiz.Models.Question;

namespace quiz.Services;

public partial class QuestionService 
{
     public static Question ToModel(Entities.Question entity)
    => new()
    {
        Id = entity.Id,
        Title = entity.Title,
        Description = entity.Description,
        TopicId = entity.TopicId,
        TimeAllowed = entity.TimeAllowed,
        Type = ToModel(entity.Type),
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt
    };
     public static EQuestionType ToModel(Entities.EQuestionType entity)
    => entity switch
    {
        Entities.EQuestionType.Algorithmic => EQuestionType.Algorithmic,
        _ => EQuestionType.MultipleChoice,
    };
    private static Entities.EQuestionType ToEntity(EQuestionType entity)
    {
        return entity switch
        {
            EQuestionType.MultipleChoice => Entities.EQuestionType.MultipleChoice,
            _=> Entities.EQuestionType.Algorithmic
        };
    }

    private static Entities.EDifficulty ToDifficultyEntity(Models.Topic.EDifficulty difficulty)
    => difficulty switch
    {
        Models.Topic.EDifficulty.Beginner => Entities.EDifficulty.Beginner,
        Models.Topic.EDifficulty.Intermediate => Entities.EDifficulty.Intermediate,
        _ => Entities.EDifficulty.Advanced
    };

    private static Expression<Func<Entities.Question, bool>> QuestionFilter(string search, string topic,Models.Topic.EDifficulty difficulty,bool booldifficulty)
    {
        Expression<Func<Entities.Question, bool>> filter = question
        => question.Title!.Contains(search)
        && question.Topic != null 
        && question.Topic.Name!.ToLower().Contains(topic)
        && ((int) question.Topic.Difficulty == (int)ToDifficultyEntity( difficulty ) || !(booldifficulty));

        return filter;
    }
}