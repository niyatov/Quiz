using quiz.Models;
using quiz.Models.Topic;
namespace quiz.Services;
public interface ITopicService 
{
    ValueTask<Result<List<Topic>>> GetAllPagedTopics();
    ValueTask<Result<Topic>> GetByIdAsync(ulong id);
    ValueTask<Result<Topic>> FindByNameAsync(string name);
    ValueTask<Result<Topic>> RemoveByIdAsync(ulong id);
    ValueTask<Result> CreateAsync(TopicCreated topic);
    ValueTask<Result<Topic>> UpdateAsync(ulong id, TopicCreated topic);
    ValueTask<bool> ExistsAsync(ulong id);






    
}