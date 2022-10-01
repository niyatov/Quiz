namespace quiz.Entities;
public class Topic : EntityBase
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public EDifficulty Difficulty { get; set; }
    public string? NameHash { get; set; }
    public virtual ICollection<Question>? Questions { get; set; }
}