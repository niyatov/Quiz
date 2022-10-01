using System.ComponentModel.DataAnnotations;

namespace quiz.Dtos;
public class Topic
{
    public ulong Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public string? Difficulty { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}