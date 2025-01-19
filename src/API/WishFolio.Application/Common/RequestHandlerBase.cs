using MediatR;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Application.Common;

public abstract class RequestHandlerBase<TRequest> : IRequestHandler<TRequest, Result>
    where TRequest : RequestBase
{
    public abstract Task<Result> Handle(TRequest request, CancellationToken cancellationToken);

    public Result Ok()
    {
        return Result.Ok();
    }

    public Result Failure(Error error)
    {
        return Result.Failure(error);
    }
}

public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>>
    where TRequest : RequestBase<TResponse>
    where TResponse : class
{
    public abstract Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);

    public Result<TResponse> Ok(TResponse value)
    {
        return Result<TResponse>.Ok(value);
    }

    public Result<TResponse> Failure(Error error)
    {
        return Result<TResponse>.Failure(error);
    }
}
