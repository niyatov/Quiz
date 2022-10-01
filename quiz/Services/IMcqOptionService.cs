using quiz.Models;
using quiz.Models.McqOption;

namespace quiz.Services;

public interface IMcqOptionService
{
    ValueTask<Result<List<McqOption>>> GetAllOptionsByQuestionIdAsync(ulong questionId);  
    ValueTask<Result> CreateOptionsAsync(List<McqOption> models, ulong questionId);
    ValueTask<Result<McqOption>> RemoveOptionsByQuestionIdAsync(ulong questionId); 
}