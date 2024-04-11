namespace StratmanMedia.Utilities.Results;

public class Result<T>
{
    public ResultStatus Status = ResultStatus.Success;
    public IEnumerable<string> ErrorMessages { get; init; } = Array.Empty<string>();
    public bool IsSuccess => Status == ResultStatus.Success;
    public string? CorrelationId { get; init; }
    public T? Data { get; init; }

    protected Result() { }

    public static Result<T> Success(T? data = default)
    {
        return new Result<T>{
            Data = data
        };
    }

    public static Result<T> Success(string correlationId, T? data = default)
    {
        return new Result<T>{
            Data = data,
            CorrelationId = correlationId
        };
    }

    public static Result<T> Error(string message)
    {
        return new Result<T>
        {
            Status = ResultStatus.Error,
            ErrorMessages = new []{message}
        };
    }

    public static Result<T> Invalid(IEnumerable<string> messages)
    {
        return new Result<T>
        {
            Status = ResultStatus.Invalid,
            ErrorMessages = messages
        };
    }

    public static Result<T> NotFound()
    {
        return new Result<T>
        {
            Status = ResultStatus.NotFound
        };
    }
}