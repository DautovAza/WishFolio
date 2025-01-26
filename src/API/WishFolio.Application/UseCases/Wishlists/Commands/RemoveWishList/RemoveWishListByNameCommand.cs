using WishFolio.Application.Common;

namespace WishFolio.Application.UseCases.Wishlists.Commands.RemoveWishList;

public record RemoveWishListByNameCommand(string Name) : RequestBase;
