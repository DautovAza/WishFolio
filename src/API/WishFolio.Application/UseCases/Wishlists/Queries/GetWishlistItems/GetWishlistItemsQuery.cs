using WishFolio.Application.Common;
using WishFolio.Application.UseCases.Wishlists.Queries.Dtos;

namespace WishFolio.Application.UseCases.Wishlists.Queries.GetWishlistItems;

public record GetWishlistItemsQuery(Guid UserId, string WishListName) : RequestBase<IEnumerable<WishListItemDto>>;
