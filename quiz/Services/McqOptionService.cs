using Microsoft.EntityFrameworkCore;
using quiz.Models;
using quiz.Models.McqOption;
using quiz.Repositories;

namespace quiz.Services;

public partial class McqOptionService : IMcqOptionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<McqOptionService> _logger;

    public McqOptionService(
        IUnitOfWork unitOfWork,
        ILogger<McqOptionService> logger)
    {

        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async ValueTask<Result> CreateOptionsAsync(List<McqOption> models, ulong questionId)
    {
        if (models.Count < 1)
            return new("Options is invalid");

        var question = _unitOfWork.Questions.GetAll().FirstOrDefault(q => q.Id == questionId);
        if (question is null)
            return new("Question with given Id not found.");

        if (question?.Type == Entities.EQuestionType.Algorithmic)
            return new("Question type is algorithmic");

        var hasOptionQuestion = await _unitOfWork.McqOptions.GetAll().FirstOrDefaultAsync(b => b.QuestionId == questionId);
        if (hasOptionQuestion is not null) 
            return new("Options are already exist");

        try
        {
            ulong i= 0;
            var entityOptions = models.Select(m => ToEntity(m, questionId,++i)).ToList(); 
            // i ni o'zi berish qo'pol xato, o'shanda i har safar nol beradi. Qiymay o'zgarmaydi , uyoqdagi o'zgarish bunga tasir qilmaydi

            foreach (var option in entityOptions)
            {
                await _unitOfWork.McqOptions.AddAsync(option);
            }
            return new(true);
        }
        catch(DbUpdateException e)
        {
            return new($"Couldn't create mcqoption. Contact support.. {e}");
        }
        catch (Exception e)
        {
            throw new("Couldn't create mcqoption.", e);
        }
    }

    public async ValueTask<Result<List<McqOption>>> GetAllOptionsByQuestionIdAsync(ulong questionId)
    {
        try
        {
            var existingMcqOption = _unitOfWork.McqOptions.GetAll()
                .Where(b => b.QuestionId == questionId);

            if (existingMcqOption.Count() < 1)
                return new("No mcqOptions found for this questionId");

            var modelMcqOption = await existingMcqOption.Select(q => ToModel(q)).ToListAsync();

            return new(true) { Data = modelMcqOption };
        }
        catch(DbUpdateException e)
        {
            return new($"Couldn't create mcqoption. Contact support.. {e}");
        }
        catch (Exception e)
        {
            throw new("Couldn't get mcqOption. Contact support.", e);
        }
    }

    public async ValueTask<Result<McqOption>> RemoveOptionsByQuestionIdAsync(ulong questionId)
    {
        try
        {
            var existingMcqOption = _unitOfWork.McqOptions.GetAll()
                 .Where(b => b.QuestionId == questionId);

            if (existingMcqOption.Count() < 1)
                return new("No mcqOptions found. contact support.");

            await _unitOfWork.McqOptions.RemoveRange(existingMcqOption);

            return new(true);
        }
        catch(DbUpdateException e)
        {
            return new($"Couldn't create mcqoption. Contact support.. {e}");
        }
        catch (Exception e)
        {
            throw new("Couldn't Delete mcqoption. Contact support.", e);
        }
    }

}