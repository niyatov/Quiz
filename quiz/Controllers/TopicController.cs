using Microsoft.AspNetCore.Mvc;
using quiz.Data;
using quiz.Dtos;
using quiz.Services;

namespace quiz.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TopicController : ControllerBase
{
    private readonly ITopicService _topicService;

    public TopicController(ITopicService topicService)
    {
        _topicService = topicService;
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetTopics()
    {
        var entity = await _topicService.GetAllPagedTopics();
        if(!entity.IsSuccess)
            return NotFound(new {ErrorMessage = entity.ErrorMessage});
        return Ok(entity?.Data?.Select(x => ToDto(x)));
    }

    [HttpPost]
    public async ValueTask<ActionResult> Create(TopicCreate topic)
    {
        var entity = await _topicService.CreateAsync(ToTopicCreatedModel(topic));
        if(!entity.IsSuccess)
             return NotFound(new {ErrorMessage = entity.ErrorMessage});
        return Ok();
    }

    [HttpPut]
    public async ValueTask<ActionResult> Update(ulong id,TopicCreate topic)
    {
        var entity = await _topicService.UpdateAsync(id,ToTopicCreatedModel(topic));
        if(!entity.IsSuccess)
            return NotFound(new {ErrorMessage = entity.ErrorMessage});
        return Ok(ToDto(entity.Data!));
    }

    [HttpGet]
    [Route("{id}")]
    public async ValueTask<ActionResult> GetByIdAsync(ulong id)
    {
        var entity =await  _topicService.GetByIdAsync(id);
        if(!entity.IsSuccess)
            return NotFound(new {ErrorMessage = entity.ErrorMessage});
        return Ok(ToDto(entity.Data!));
    }

    [HttpGet]
    [Route("Name")]
    public async ValueTask<ActionResult> GetByNameAsync(string name)
    {
        var entity = await _topicService.FindByNameAsync(name);
        if(!entity.IsSuccess)
             return NotFound(new {ErrorMessage = entity.ErrorMessage});
        return Ok(ToDto(entity.Data!));
    }
    [HttpDelete]
    public async ValueTask<ActionResult> DeleteAsync(ulong id)
    {
        var entity =await _topicService.RemoveByIdAsync(id);
        if(!entity.IsSuccess)
            return NotFound(new {ErrorMessage = entity.ErrorMessage});
        return Ok(ToDto(entity.Data!));
    }

    private static  Models.Topic.TopicCreated ToTopicCreatedModel(TopicCreate topic)
    {
        return new Models.Topic.TopicCreated()
        {
            Name = topic.Name,
            Description = topic.Description,
            Difficulty = ToTopicCreatedModel(topic.Difficulty),
        };
    }
    private static Models.Topic.EDifficulty ToTopicCreatedModel(EDifficulty difficulty)
    {
        return difficulty switch 
        {
            EDifficulty.Beginner => Models.Topic.EDifficulty.Beginner,
            EDifficulty.Intermediate => Models.Topic.EDifficulty.Intermediate,
            _=> Models.Topic.EDifficulty.Intermediate,
        };
    }

    private static Topic ToDto(Models.Topic.Topic topic)
    {
        return new Topic()
        {
            Id = topic.Id,
            Name = topic.Name,
            Description = topic.Description,
            Difficulty = topic.Difficulty,
            CreatedAt = topic.CreatedAt,
            UpdatedAt = topic.UpdatedAt
        };
    }
}