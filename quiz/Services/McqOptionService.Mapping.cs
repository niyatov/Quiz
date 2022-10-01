using quiz.Models.McqOption;

namespace quiz.Services;

public partial class McqOptionService
{

    public static McqOption ToModel(Entities.McqOption entity)
    => new()
    {
        Id = entity.Id,
        QuestionId = entity.QuestionId,
        IsTrue = entity.IsTrue,
        Content = entity.Content 
    };

    public  Entities.McqOption ToEntity(McqOption model, ulong questionId,ulong i)
    {
        return new Entities.McqOption(model.Id+i, questionId, model.IsTrue, model.Content);
    }
}