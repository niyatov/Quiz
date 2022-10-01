namespace quiz.Repositories;

public interface IUnitOfWork : IDisposable
{
    QuizRepository Quizzes { get; }
    int Save();
}