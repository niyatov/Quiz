
using quiz.Models;
using quiz.Models.Quiz;

namespace quiz.Services;
public interface IQuizService
{
    ValueTask<Result<List<Quiz>>> GetAllQuizzesPaginatedAsync(int page,int limit);
    ValueTask<Result<Quiz>> GetByIdAsync(ulong id);
    ValueTask<Result> CreateAsync(string title, string description,DateTimeOffset startTime, DateTimeOffset endTime,string? password = null);
    ValueTask<Result> RemoveByIdAsync(ulong id);
    ValueTask<Result> UpdateAsync(ulong id, string title, string description, DateTimeOffset startTime, DateTimeOffset endTime,string? password = null);
}