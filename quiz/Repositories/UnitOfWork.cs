using quiz.Data;

namespace quiz.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public QuizRepository Quizzes {get;}
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Quizzes = new QuizRepository(context);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask SaveAsync()
    {
         await _context.SaveChangesAsync();
    }

    public int Save()
    {
        throw new NotImplementedException();
    }
}