using quiz.Models;
using quiz.Models.Question;

namespace quiz.Services;
public interface IQuestionService
{
    ValueTask<Result<List<Question>>> GetAllQuestionsAsync(int page, int limit,string? topic = null, string? search = null, Models.Topic.EDifficulty difficulty = Models.Topic.EDifficulty.Beginner, bool booldifficulty = false);
    ValueTask<Result<Question>> GetByIdAsync(ulong id);
    ValueTask<Result<Question>> FindByTitleAsync(string title);
    ValueTask<Result<Question>> CreateAsync(ulong topicId, string title, string description, EQuestionType type, int timeAllowed);
    ValueTask<Result<Question>> UpdateAsync(ulong topicId, ulong id, string title, string description, EQuestionType type, int timeAllowed);
    ValueTask<Result<Question>> RemoveByIdAsync(ulong id);
    ValueTask<bool> ExistsAsync(ulong id);
}