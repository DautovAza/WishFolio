using WishFolio.Application.Common;
using WishFolio.Application.UseCases.Wishlists.Commands.Items.Abstractions;

namespace WishFolio.Application.UseCases.Wishlists.Commands.Items.RemoveWishListItem;

public record RemoveWishListItemCommand(string WishListName, Guid Id)
    : RequestBase, IWishListItemCommand;
