using WishFolio.Domain.Abstractions.Entities;

namespace WishFolio.Application.Common;

public abstract record PagedRequestBase<TEntity>(int PageNumber, int PageSize) : RequestBase<PagedCollection<TEntity>>
    where TEntity : class;
