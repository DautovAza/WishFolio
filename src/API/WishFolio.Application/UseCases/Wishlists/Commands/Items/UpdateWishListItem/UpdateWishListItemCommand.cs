using WishFolio.Application.Common;
using WishFolio.Application.UseCases.Wishlists.Commands.Items.Abstractions;

namespace WishFolio.Application.UseCases.Wishlists.Commands.Items.UpdateWishListItem;

public record UpdateWishListItemCommand(string Name, string Description, string LinkUrl)
    : RequestBase, IWishListItemCommand
{
    public string WishListName { get; set; }
    public Guid Id { get; set; }
}