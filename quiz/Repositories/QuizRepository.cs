using quiz.Data;
using quiz.Entities;

namespace quiz.Repositories;
public class QuizRepository : GenericRepository<Quiz>, IQuizRepository
{
    public QuizRepository(ApplicationDbContext context) : base(context)
    {
    }
}