namespace quiz.Entities;
public class EntityBase
{
    public ulong Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}