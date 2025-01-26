using WishFolio.Application.Common;
using WishFolio.Application.UseCases.WishListItems.Commands.Abstractions;

namespace WishFolio.Application.UseCases.WishListItems.Commands.UpdateWishListItem;

public record UpdateWishListItemCommand(string WishListName, Guid Id, string Name, string Description, string LinkUrl)
    : RequestBase, IWishListItemCommand;