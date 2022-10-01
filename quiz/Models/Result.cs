namespace quiz.Models;
public class Result
{
    public bool IsSuccess;
    public string? ErrorMessage;

    public Result(string? errorMessage) => ErrorMessage = errorMessage;

    public Result(bool isSuccess) => IsSuccess = isSuccess;

}

public class Result<T> : Result
{
    public T? Data { get; set; }
    public Result(bool isSuccess) : base(isSuccess) { }
    public Result(string? errorMessage) : base(errorMessage) { }

}