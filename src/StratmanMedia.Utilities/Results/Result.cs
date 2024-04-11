namespace StratmanMedia.Utilities.Results;

public class Result
{
    public ResultStatus Status = ResultStatus.Success;
    public IEnumerable<string> ErrorMessages { get; init; } = Array.Empty<string>();
    public bool IsSuccess => Status == ResultStatus.Success;
    public string? CorrelationId { get; init; }

    protected Result() { }

    public static Result Success()
    {
        return new Result();
    }

    public static Result Success(string correlationId)
    {
        return new Result {
            CorrelationId = correlationId
        };
    }

    public static Result Error(string message)
    {
        return new Result
        {
            Status = ResultStatus.Error,
            ErrorMessages = new []{message}
        };
    }

    public static Result Invalid(IEnumerable<string> messages)
    {
        return new Result
        {
            Status = ResultStatus.Invalid,
            ErrorMessages = messages
        };
    }

    public static Result NotFound()
    {
        return new Result
        {
            Status = ResultStatus.NotFound
        };
    }
}

public class Result<T> : Result
{
    public T? Data { get; init; }

    public static Result<T> Success(T? data = default)
    {
        return new Result<T>
        {
            Data = data
        };
    }

    public static Result<T> Success(string correlationId, T? data = default)
    {
        return new Result<T>
        {
            Data = data,
            CorrelationId = correlationId
        };
    }
}