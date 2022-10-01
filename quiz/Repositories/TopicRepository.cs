using quiz.Data;
using quiz.Entities;

namespace quiz.Repositories;
public class TopicRepository : GenericRepository<Topic>, ITopicRepository
{
    public TopicRepository(ApplicationDbContext context) : base(context)
    {
    }
}