using WishFolio.Application.Common;
using WishFolio.Application.UseCases.Wishlists.Commands.Items.Abstractions;

namespace WishFolio.Application.UseCases.Wishlists.Commands.Items.AddWishListItem;

public record AddItemToWishListCommand(string Name,
                                       string Description,
                                       string LinkUrl)
    : RequestBase, IWishListItemCommand
{
    public string WishListName { get; set; }
}
