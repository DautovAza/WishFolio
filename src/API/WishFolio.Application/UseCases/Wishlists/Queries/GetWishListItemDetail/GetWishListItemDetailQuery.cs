using WishFolio.Application.Common;
using WishFolio.Application.UseCases.Wishlists.Queries.Dtos;

namespace WishFolio.Application.UseCases.WishListItems.Queries.GetWishListItemDetail;

public record GetWishListItemDetailQuery(Guid UserId, string WishListName, Guid ItemId)
    : RequestBase<WishListItemDetailsDto>;
