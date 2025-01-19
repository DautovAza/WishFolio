using MediatR;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Application.Common;

public abstract record RequestBase : IRequest<Result>
{

}

public abstract record RequestBase<T> : IRequest<Result<T>>
    where T : class
{

}
