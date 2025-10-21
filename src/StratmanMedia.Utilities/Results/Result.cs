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
        return new Result
        {
            CorrelationId = correlationId
        };
    }

    public static Result Error(string message)
    {
        return new Result
        {
            Status = ResultStatus.Error,
            ErrorMessages = new[] { message }
        };
    }

    public static Result Invalid(string message)
    {
        return new Result
        {
            Status = ResultStatus.Invalid,
            ErrorMessages = new[] { message }
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

    public static Result Unauthorized()
    {
        return new Result
        {
            Status = ResultStatus.Unauthorized
        };
    }
}

public class Result<T> : Result
{
    public T? Data { get; init; }

    protected Result() { }

    public new static Result<T> Success()
    {
        return new Result<T>();
    }

    public static Result<T> Success(T? data)
    {
        return new Result<T>
        {
            Data = data
        };
    }

    public new static Result<T> Success(string correlationId)
    {
        return new Result<T>
        {
            CorrelationId = correlationId
        };
    }

    public static Result<T> Success(T? data, string correlationId)
    {
        return new Result<T> {
            Data = data,
            CorrelationId = correlationId
        };
    }

    public new static Result<T> Error(string message)
    {
        return new Result<T>
        {
            Status = ResultStatus.Error,
            ErrorMessages = new []{message}
        };
    }

    public new static Result<T> Invalid(string message)
    {
        return new Result<T>
        {
            Status = ResultStatus.Invalid,
            ErrorMessages = new[] { message }
        };
    }

    public new static Result<T> Invalid(IEnumerable<string> messages)
    {
        return new Result<T>
        {
            Status = ResultStatus.Invalid,
            ErrorMessages = messages
        };
    }

    public new static Result<T> NotFound()
    {
        return new Result<T>
        {
            Status = ResultStatus.NotFound
        };
    }

    public new static Result<T> Unauthorized()
    {
        return new Result<T>
        {
            Status = ResultStatus.Unauthorized
        };
    }
}