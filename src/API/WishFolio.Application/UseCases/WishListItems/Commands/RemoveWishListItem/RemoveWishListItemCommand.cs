using WishFolio.Application.Common;
using WishFolio.Application.UseCases.WishListItems.Commands.Abstractions;

namespace WishFolio.Application.UseCases.WishListItems.Commands.RemoveWishListItem;

public record RemoveWishListItemCommand(string WishListName, Guid Id) 
    : RequestBase, IWishListItemCommand;
