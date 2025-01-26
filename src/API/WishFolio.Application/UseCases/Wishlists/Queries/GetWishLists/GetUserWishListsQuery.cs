using WishFolio.Application.Common;
using WishFolio.Application.UseCases.Wishlists.Queries.Dtos;

namespace WishFolio.Application.UseCases.Wishlists.Queries.GetWishLists;

public record GetUserWishListsQuery(Guid userId) : RequestBase<IEnumerable<WishListDto>>;
