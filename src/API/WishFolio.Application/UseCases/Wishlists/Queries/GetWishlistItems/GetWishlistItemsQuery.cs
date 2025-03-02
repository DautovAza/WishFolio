using WishFolio.Application.Common;
using WishFolio.Application.UseCases.Wishlists.Queries.Dtos;

namespace WishFolio.Application.UseCases.Wishlists.Queries.GetWishlistItems;

public record GetWishlistItemsQuery : PagedRequestBase<WishListItemDto>
{
    public GetWishlistItemsQuery(Guid userId, string wishListName, int pageNumber, int pageSize) 
        : base(pageNumber, pageSize)
    {
        UserId = userId;
        WishListName = wishListName;
    }

    public Guid UserId { get; }
    public string WishListName { get; }
}
