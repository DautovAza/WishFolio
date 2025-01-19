using WishFolio.Domain.Errors;

namespace WishFolio.Domain.Shared.ResultPattern;

public  class Result : IResult
{
    protected Result(bool isSuccess, Error[] errors)
    {
        bool hasErrors = errors.Any(e => !e.IsNone());
        if (isSuccess == hasErrors)
        {
            throw new ArgumentException($"Некоректное значение {nameof(Result)}. " +
                $"Объект не может одновременно иметь {nameof(isSuccess)}={isSuccess} и иметь {(hasErrors ? "не" : "")} пустую ошибку");
        }

        IsSuccess = isSuccess;
        Errors = errors.Where(e => !e.IsNone())
            .ToArray();
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public Error[] Errors { get; }

    public static Result Ok() =>
        new(true, [Error.None]);

    public static Result Failure(Error error) =>
        new(false, [error]);


    public static Result Combine(IEnumerable<IResult> results)
    {
        if (results.Any(e => e.IsFailure))
        {
            var errors = results.SelectMany(er => er.Errors)
                .ToArray();

            return new(false, errors);
        }
        return new(true, []);
    }
}

public sealed class Result<T> : Result
    where T : class
{
    private Result(T? value, bool isSuccess, Error[] errors)
        : base(isSuccess, errors)
    {
        Value = value;
    }

    public T? Value { get; }
   
    public static Result<T> Ok(T value) =>
        new(value, true, [Error.None]);

    public new static Result<T> Failure(Error error) =>
        new(null, false, [error]);

    public static Result<T> Combine(T value, IEnumerable<IResult> results)
    {
        if (results.Any(e => e.IsFailure))
        {
            var errors = results.SelectMany(er => er.Errors)
                .ToArray();

            return new(null, false, errors);
        }

        return new(value, true, []);
    }

    public static implicit operator T?(Result<T> result)
        => result.Value;  
}