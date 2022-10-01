namespace quiz.Dtos;
public class Paginated
{
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;
}