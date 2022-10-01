using Microsoft.EntityFrameworkCore;
using quiz.Models;
using quiz.Models.Question;
using quiz.Repositories;

namespace quiz.Services;
public partial class QuestionService : IQuestionService
{
    private readonly IUnitOfWork _unitOfWork;

    public QuestionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async ValueTask<Result<Question>> CreateAsync(ulong topicId, string title, string description, EQuestionType type, int timeAllowed)
    {
        if(string.IsNullOrEmpty(title))
            return new("title is invalid");
        if(string.IsNullOrEmpty(description))
            return new("description is invalid");

        var entity = _unitOfWork.Topics.GetAll().FirstOrDefault(x => x.Id == topicId);
        if(entity == null)
            return new("Not Topic Found to CreateQuestion");
        var result = new Entities.Question(topicId, title, description ,ToEntity(type),timeAllowed);
        try
        {
            await _unitOfWork.Questions.AddAsync(result);
            return new (true);
        }
        catch(DbUpdateException e)
        {
            return new($"Couldn't create topic. Contact support.. {e}");
        }
        catch (Exception e)
        {
            throw new("Couldn't create question. Contact support.", e);
        }
    }

    public async ValueTask<bool> ExistsAsync(ulong id)
    {
        var entity =await GetByIdAsync(id);
            return entity.IsSuccess;
    }

    public async ValueTask<Result<Question>> FindByTitleAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return new("Title is invalid.");
        try
        {
            var existingQuestion = await _unitOfWork.Questions.GetAll().FirstOrDefaultAsync(t => t.Title == title);
            if (existingQuestion is null)
                return new("No topic found for given name.");
            return new(true) { Data = ToModel(existingQuestion) };
        }
        catch(DbUpdateException e)
        {
            return new($"Couldn't search topic. Contact support.. {e}");
        }
        catch (Exception e)
        {
            throw new("Couldn't search question. Contact support.", e);
        }
    }

    public async ValueTask<Result<List<Question>>> GetAllQuestionsAsync(int page, int limit,string? topic = null, string? search =null, Models.Topic.EDifficulty difficulty = Models.Topic.EDifficulty.Beginner, bool booldifficulty = false)
    {
        try
        {
            var filter = QuestionFilter(search ?? string.Empty, topic?.ToLower() ?? string.Empty,difficulty,booldifficulty); 

            var existingQuestions = _unitOfWork.Questions.GetAll()
            .Include(b => b.Topic)
            .Where(filter)
            .Skip((page - 1) * limit)
            .Take(limit);

            if (existingQuestions.Count() < 1)
                return new("No Questions");

            var questions = await existingQuestions.Select(q => ToModel(q)).ToListAsync();

            return new(true) { Data = questions };
        }
        catch(DbUpdateException e)
        {
            return new($"Couldn't get questions. Contact support.. {e}");
        }
        catch (Exception e)
        {
            throw new("Couldn't get questions. Contact support.", e);
        }
    }


    public async ValueTask<Result<Question>> GetByIdAsync(ulong id)
    {
        try
        {
            var existingQuestion = await _unitOfWork.Questions.GetByIdAsync(id);

            if (existingQuestion is null)
                return new("Question with given Id Not Found.");

            return new(true) { Data = ToModel(existingQuestion) };

        }
        catch(DbUpdateException e)
        {
            return new($"Couldn't get questions. Contact support.. {e}");
        }
        catch (Exception e)
        {
            throw new("Couldn't get questions. Contact support.", e);
        }
    }

    public async ValueTask<Result<Question>> RemoveByIdAsync(ulong id)
    {
         try
        {
            var existingQuestion =await _unitOfWork.Questions.GetByIdAsync(id);

            if (existingQuestion is null)
                return new("Question with given Id Not Found.");
            var entity = await _unitOfWork.Questions.Remove(existingQuestion);

            return new(true){Data = ToModel(entity)};
        }
        catch(DbUpdateException e)
        {
            return new($"Couldn't get questions to remove. Contact support.. {e}");
        }
        catch (Exception e)
        {
            throw new("Couldn't get questions to remove. Contact support.", e);
        }
    }

    public async ValueTask<Result<Question>> UpdateAsync(ulong topicId, ulong id, string title, string description, EQuestionType type, int TimeAllowed)
    {
        var existingQuestion =await _unitOfWork.Questions.GetByIdAsync(id);
        if (existingQuestion is null)
            return new("Question with given ID not found.");
        
        var entity = _unitOfWork.Topics.GetAll().FirstOrDefault(x => x.Id == topicId);
        if(entity == null)
            return new("Not Topic Found to UpdateQuestion");

        existingQuestion.Title = title;
        existingQuestion.Description = description;
        existingQuestion.TopicId = topicId;
        existingQuestion.TimeAllowed = TimeAllowed;
        existingQuestion.Type = ToEntity(type);

        try
        {
            var updatedQuestion = await _unitOfWork.Questions.Update(existingQuestion);
            return new(true) { Data = ToModel(updatedQuestion) };
        }
        catch(DbUpdateException e)
        {
            return new($"Couldn't get questions to remove. Contact support.. {e}");
        }
        catch (Exception e)
        {
            throw new("Couldn't update question. Contact support.", e);
        }
    }
}