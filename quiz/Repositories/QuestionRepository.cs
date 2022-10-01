using quiz.Data;
using quiz.Entities;

namespace quiz.Repositories;
public class QuestionRepository : GenericRepository<Question>,IQuestionRepository
{
    public QuestionRepository(ApplicationDbContext context) : base(context)
    {
    }
}