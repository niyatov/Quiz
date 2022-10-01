using System.ComponentModel.DataAnnotations;
using quiz.Dtos.Question;

namespace quiz.Dtos.Question;

public class CreateQuestionDto
{
    [Required, MaxLength(255)]
    public string? Title { get; set; }
    
    [Required, MaxLength(int.MaxValue)]
    public string? Description { get; set; }
    
    [Required]
    public EQuestionType Type { get; set; }
    
    [Required]
    public int TimeAllowed { get; set; }
    
    [Required]
    public ulong TopicId { get; set; }
}