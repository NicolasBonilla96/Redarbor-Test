using System.Net;

namespace Redarbor.Application.Common.Results;

public class Result<T>
{
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public string Error { get; }

    public T Value { get; }

    public int Code { get; }

    private Result(bool isSuccess, string error, T value, int code)
    {
        IsSuccess = isSuccess;
        Error = error;
        Value = value;
        Code = code;
    }

    public static Result<T> Success(T value)
        => new(true, null, value, default);

    public static Result<T> Failure(string error, int code)
        => new(false, error, default, code);

    public static Result<T> Craete(T value)
        => value is null
            ? Failure("Value cannot be null", (int)HttpStatusCode.BadRequest)
            : Success(value);

    public async Task<Result<K>> Bind<K>(Func<T, Task<Result<K>>> func)
        => IsFailure ? Result<K>.Failure(Error, Code) : await func(Value);

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<string, TResult> onFailure)
        => IsSuccess ? onSuccess(Value) : onFailure(Error);
}
