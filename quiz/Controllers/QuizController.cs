namespace quiz.Controllers;

using System;
using Microsoft.AspNetCore.Mvc;
using quiz.Dtos;
using quiz.Services;

[ApiController]
[Route("/api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }
    [HttpGet]
    public async Task<ActionResult> Get([FromQuery]Paginated paginated)
    {
        try
        {
            var entity = await _quizService.GetAllQuizzesPaginatedAsync(paginated.Page, paginated.Limit);
            if(!entity.IsSuccess)
                return NotFound(new {ErrorMessage = entity.ErrorMessage });
            return Ok(entity?.Data?.Select(x => ToDto(x)));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new {ErrorMessage = e.Message});
        }
    }

    private static Quiz ToDto(Models.Quiz.Quiz entity)
    {
        return new Quiz(entity.Id, entity.Title, entity.Description,
            entity.StartTime, entity.EndTime, entity.CreatedAt, entity.UpdatedAt);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateQuiz quiz)
    {
        try
        {
            var entity =await _quizService.CreateAsync(quiz.Title!, quiz.Description!, quiz.StartTime,quiz.EndTime,quiz.Password);
            if(!entity.IsSuccess)
                return NotFound(new {ErrorMessage = entity.ErrorMessage});
            return Ok();
        }
        catch (Exception e)
        {
             return StatusCode(StatusCodes.Status500InternalServerError, new {ErrorMessage = e.Message});
        }

    }
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> Get(ulong id)
    {
        try
        {
            var entity = await _quizService.GetByIdAsync(id);
        if(!entity.IsSuccess)
            return NotFound(new {ErrorMessage = entity.ErrorMessage});
        return Ok(ToDto(entity.Data!));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new{ErrorMessage = e.Message});
        }

    }
    [HttpDelete]
    public async Task<ActionResult> Remove(ulong id)
    {
        try
        {
            var entity = await _quizService.RemoveByIdAsync(id);
        if(!entity.IsSuccess)
            return NotFound(new {ErrorMessage = entity.ErrorMessage });
        return Ok();
        }
        catch(Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new{ErroMessage = e.Message});
        }

    }
    [HttpPut]
    public async Task<ActionResult> Update(ulong id,CreateQuiz quiz)
    {
        try
        {
            var entity =await _quizService.UpdateAsync(id,quiz.Title!,quiz.Description!,quiz.StartTime,quiz.EndTime,quiz?.Password);
        if(!entity.IsSuccess)
            return NotFound(new {ErrorMessage = entity.ErrorMessage });
        return Ok();
        }
        catch (Exception e)
        {
             return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
        }
    }
}