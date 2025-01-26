using WishFolio.Application.Common;
using WishFolio.Application.UseCases.WishListItems.Commands.Abstractions;

namespace WishFolio.Application.UseCases.WishListItems.Commands.AddWishListItem;

public record AddItemToWishListCommand(string WishListName, string Name, string Description, string LinkUrl) 
    : RequestBase, IWishListItemCommand;
