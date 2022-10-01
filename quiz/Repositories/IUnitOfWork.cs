namespace quiz.Repositories;

public interface IUnitOfWork : IDisposable
{
    QuizRepository Quizzes { get; }
    TopicRepository Topics { get; }
    QuestionRepository Questions { get; }
    int Save();
}