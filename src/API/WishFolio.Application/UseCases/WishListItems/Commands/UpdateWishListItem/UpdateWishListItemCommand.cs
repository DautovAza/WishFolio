using WishFolio.Application.Common;
using WishFolio.Application.UseCases.WishListItems.Commands.Abstractions;

namespace WishFolio.Application.UseCases.WishListItems.Commands.UpdateWishListItem;

public record UpdateWishListItemCommand(string Name, string Description, string LinkUrl)
    : RequestBase, IWishListItemCommand
{
    public string WishListName { get; set; }
    public Guid Id { get; set; }
}