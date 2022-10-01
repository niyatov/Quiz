using Microsoft.EntityFrameworkCore;
using quiz.Models;
using quiz.Models.Quiz;
using quiz.Repositories;
using quiz.Utils;

namespace quiz.Services;
public  partial class QuizService : IQuizService
{
    private readonly IUnitOfWork _unitOfWork;

    public QuizService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async ValueTask<Result> CreateAsync(string title, string description, DateTimeOffset startTime, DateTimeOffset endTime, string? password = null)
    {
        if(title == null)
            return new("title is invalid");
        if(description == null)
            return new("description is invalid");
        Entities.Quiz entity;
        if (password == null)
        {
            entity = new Entities.Quiz()
            {
                Title = title,
                Description = description,
                StartTime = startTime,
                EndTime = endTime,
            };
        }
        else
        {
            entity = new Entities.Quiz()
            {
                Title = title,
                Description = description,
                StartTime = startTime,
                EndTime = endTime,
                PasswordHash = password.Sha256()
            };
        }

        try
        {
            await _unitOfWork.Quizzes.AddAsync(entity);
            return new(true);
        }
        catch(DbUpdateException e)
        {
            return new($"Couldn't create quiz. Contact support.. {e}");
        }
        catch (Exception e)
        {
            throw new("Couldn't create quiz. Contact support.", e);
        }
    }

    public async ValueTask<Result<List<Quiz>>> GetAllQuizzesPaginatedAsync(int page, int limit)
    {
        try
        {
        var entity =_unitOfWork.Quizzes.GetAll();
        if(entity.Count() < 1)
            return new("Quizz is empty.");
        var result = await entity
            .Skip((page-1)*limit)
            .Take(limit)
            .Select(x =>ToModel(x)).ToListAsync();
        return new(true){ Data = result};
         //it works without ToListAsync (that is with ToList()), but yellow warming will appears
        }
        catch (Exception e)
        {
            throw new("Couldn't get quizzes. Contact support.", e);
        }
    }

    public async ValueTask<Result<Quiz>> GetByIdAsync(ulong id)
    {
        try
        {
        var entity = await _unitOfWork.Quizzes.GetByIdAsync(id);
        if(entity == null)
            return new("not Quiz which has this id");
        return new(true){Data = ToModel(entity)};
        }
        catch(DbUpdateException e)
        {
            return new($"Couldn't create quiz. Contact support.. {e}");
        }
        catch (Exception e)
        {
            throw new("Couldn't get quiz by id. Contact support.", e);
        }
    }

    public async ValueTask<Result> RemoveByIdAsync(ulong id)
    {
        try
        {
        var entity = await _unitOfWork.Quizzes.GetByIdAsync(id);
        if(entity == null)
            return new("not Quiz which has id to remove");
        await _unitOfWork.Quizzes.Remove(entity);
        return new(true);
        }
        catch(DbUpdateException e)
        {
            return new($"Couldn't remove quiz. Contact support.. {e}");
        }
        catch (Exception e)
        {
            throw new ("Couldn't remove quiz. Contact support.", e);
        }
    }

    public async ValueTask<Result> UpdateAsync(ulong id,string title, string description, DateTimeOffset startTime, DateTimeOffset endTime, string? password = null)
    {
        if(string.IsNullOrEmpty(title))
            return new("title is invalid");
        if(string.IsNullOrEmpty(description))
            return new("description is invalid");

        var entity = await _unitOfWork.Quizzes.GetByIdAsync(id);
        if(entity == null)
            return new("not Quiz which has this id to update");
        if((entity.Title == title) && (entity.Description == description) && (entity.StartTime == startTime) 
            && (entity.EndTime == endTime) && (entity.PasswordHash == password?.Sha256()))
            return new("quiz is the same as older");

        entity.Title = title;
        entity.Description = description;
        entity.StartTime = startTime;
        entity.EndTime = endTime;

        if(password != null)
            entity.PasswordHash = password.Sha256();
        try
        {
            await _unitOfWork.Quizzes.Update(entity);
        return new(true);
        }
        catch(DbUpdateException e)
        {
            return new($"Couldn't update quiz. Contact support.. {e}");
        }
        catch (Exception e)
        {
            throw new ("Couldn't update quiz. Contact support.", e);
        }
    }
}