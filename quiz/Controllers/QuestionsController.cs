using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using quiz.Dtos;
using quiz.Dtos.Question;
// using quiz.Dtos.McqOption;

using quiz.Services;

namespace quiz.Controllers;

[ApiController]
[Route("api/[controller]")]
public partial class QuestionsController : ControllerBase
{
    private readonly IQuestionService _questionService;
    private readonly IMcqOptionService _mcqOptionService;

    public QuestionsController(
        IQuestionService questionService,
        IMcqOptionService mcqOptionService)
    {
        _questionService = questionService;
        _mcqOptionService = mcqOptionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetQuestions([FromQuery] Filter filter)
    {
        try
        {
            var booldifficulty = Enum.TryParse(filter.Difficulty, true, out Models.Topic.EDifficulty difficulty);

            if(booldifficulty is false && filter.Difficulty != null)
                return Ok("Not this Difficulty type");

            var questionsResult = await _questionService.GetAllQuestionsAsync(
                page: filter.Page ?? 1,
                limit: filter.Limit ?? 100,
                topic: filter.Topic,
                search: filter.Search,
                difficulty: difficulty,
                booldifficulty : booldifficulty
            );

            if (!questionsResult.IsSuccess)
                return NotFound(new { ErrorMessage = questionsResult.ErrorMessage });

            return Ok(questionsResult?.Data?.Select(ToDto));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetQuestion([FromRoute]ulong id)
    {
        try
        {
            var questionResult = await _questionService.GetByIdAsync(id);

            if (!questionResult.IsSuccess)
                return NotFound(new { ErrorMessage = questionResult.ErrorMessage });

            return Ok(ToDto(questionResult.Data!));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
        }
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetQuestionByTitle([FromQuery]string? title)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest(new { ErrorMessage = "Title is required to search by." });

            var questionResult = await _questionService.FindByTitleAsync(title);

            if (!questionResult.IsSuccess || questionResult.Data is null)
                return NotFound(new { ErrorMessage = questionResult.ErrorMessage });

            return Ok(ToDto(questionResult.Data));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostQuestion(CreateQuestionDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(model);

        try
        {
            var createQuestionResult = await _questionService.CreateAsync(model.TopicId,model.Title!, model.Description!, ToModel(model.Type), model.TimeAllowed);
            if (!createQuestionResult.IsSuccess)
                return BadRequest(new { ErrorMessage = createQuestionResult.ErrorMessage });

            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteQuestion(ulong id)
    {
        try
        {
            var removeQuestionResult = await _questionService.RemoveByIdAsync(id);

            if (!removeQuestionResult.IsSuccess || removeQuestionResult.Data is null)
                return NotFound(new { ErrorMessage = removeQuestionResult.ErrorMessage });

            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateQuestion([FromRoute] ulong id, [FromBody] UpdateQuestionDto model)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(model);

            var updateQuestionResult = await _questionService.UpdateAsync(model.TopicId,id, model.Title!, model.Description!, ToModel(model.Type), model.TimeAllowed);
           
            if (!updateQuestionResult.IsSuccess)
                return BadRequest(new { ErrorMessage = updateQuestionResult.ErrorMessage });

            return Ok(ToDto(updateQuestionResult?.Data!));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
        }
    }
    
    private Question ToDto(Models.Question.Question model)
    => new()
    {
        Id = model.Id,
        Title = model.Title,
        Description = model.Description,
        Type = ToDto(model.Type).ToString(),
        TimeAllowed = model.TimeAllowed,
        TopicId = model.TopicId,
        CreatedAt = model.CreatedAt,
        UpdatedAt = model.UpdatedAt,
    };

    private EQuestionType ToDto(Models.Question.EQuestionType model)
    => model switch
    {
        Models.Question.EQuestionType.MultipleChoice => EQuestionType.MultipleChoice,
        _ => EQuestionType.Algorithmic,
    };

    private Models.Question.EQuestionType ToModel(EQuestionType dto)
    => dto switch
    {
        EQuestionType.MultipleChoice => Models.Question.EQuestionType.MultipleChoice,
        _ => Models.Question.EQuestionType.Algorithmic,
    };
}