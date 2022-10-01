using System.ComponentModel.DataAnnotations;

namespace quiz.Dtos;
public class TopicCreate
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public EDifficulty Difficulty { get; set; }
}
