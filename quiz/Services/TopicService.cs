
using System.Linq;
using Microsoft.EntityFrameworkCore;
using quiz.Models;
using quiz.Models.Topic;
using quiz.Repositories;
using quiz.Utils;

namespace quiz.Services;
public partial class TopicService : ITopicService
{
    private readonly IUnitOfWork _unitOfWork;

    public TopicService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async ValueTask<Result> CreateAsync(TopicCreated  topic)
    {
        if(string.IsNullOrWhiteSpace(topic.Name))
            return new ("Name is invalid.");
        if(string.IsNullOrWhiteSpace(topic.Description))
            return new ("Description is invalid.");
        try
        {
            var entity = ToEntity(topic);
            entity.NameHash = entity.Name?.Sha256();
            await _unitOfWork.Topics.AddAsync(entity);
            return new(true);
        }
        catch(DbUpdateException e)
        {
            return new($"Couldn't create topic. Contact support.. {e}");
        }
        catch (Exception e)
        {
            return new ($"Couldn't create topic. Contact support. {e}");
        }
    }

    public async ValueTask<Result<Topic>> UpdateAsync(ulong id, TopicCreated topic)
    {
        if(string.IsNullOrWhiteSpace(topic.Name))
            return new ("Name is invalid.");
        if(string.IsNullOrWhiteSpace(topic.Description))
            return new ("Description is invalid.");

        var entity =await  _unitOfWork.Topics.GetByIdAsync(id);
        if(entity is null)
            return new("Not Topic which has this id");

        if((entity.Name?.ToLower() == topic.Name?.ToLower()) && (entity.Description == topic.Description) && (entity.Difficulty == ToEntity(topic.Difficulty)))
            return new ("This topic is the same with older");

        entity.Name = topic.Name;
        entity.Description = topic.Description;
        entity.Difficulty = ToEntity(topic.Difficulty);
        try
        {
            entity.NameHash = entity.Name?.Sha256();
            var result = await _unitOfWork.Topics.Update(entity);
            return new (true){Data = ToModel(result) };
        }
        catch(DbUpdateException e)
        {
            return new($"Couldn't create topic. Contact support.. {e}");
        }
        catch (Exception e)
        {
            return new ($"Couldn't create topic. Contact support. {e}");
        }
    }

    public async ValueTask<bool> ExistsAsync(ulong id)
    {
        var entity = await GetByIdAsync(id); // this method belongs this service
        return entity.IsSuccess;
    }

    public async ValueTask<Result<Topic>> FindByNameAsync(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
            return new ("Name is invalid.");

        var nameHash = name.ToLower().Sha256();

        try
        {
            var entity = await  _unitOfWork.Topics.GetAll().FirstOrDefaultAsync(x => x.NameHash == nameHash);
            if(entity is null)
            return new("Not topic which has name");

            return new(true){Data = ToModel(entity)};
        }
        catch(DbUpdateException e)
        {
            return new($"Couldn't create topic. Contact support.. {e}");
        }
        catch (Exception e)
        {
            return new ($"Couldn't create topic. Contact support. {e}");
        }
    }

    public async ValueTask<Result<List<Topic>>> GetAllPagedTopics()
    {
            var entity = _unitOfWork.Topics.GetAll();
            if(entity.Count() < 1)
                return new("Topic is empty.");

            var result = await entity.Select(b => ToModel(b)).ToListAsync();
            return new (true) {Data = result};
    }

    public async ValueTask<Result<Topic>> GetByIdAsync(ulong id)
    {
        var entity = await _unitOfWork.Topics.GetByIdAsync(id);
        if(entity is null)
            return new("Not topic which has id");
        return new(true) {Data = ToModel(entity)};
    }

    public async ValueTask<Result<Topic>> RemoveByIdAsync(ulong id)
    {
        var entity = await _unitOfWork.Topics.GetByIdAsync(id);
        if(entity is null)
            return new("Not topic which has id to remove");
        try
        {
            var entry = await _unitOfWork.Topics.Remove(entity);
            return new(true){Data = ToModel(entry)};
        }
        catch(DbUpdateException e)
        {
            return new($"Couldn't create topic. Contact support.. {e}");
        }
        catch (Exception e)
        {
            return new ($"Couldn't create topic. Contact support. {e}");
        }
    }
}