using WishFolio.Application.Common;
using WishFolio.Domain.Entities.WishListAgregate;

namespace WishFolio.Application.UseCases.Wishlists.Commands.UpdateWishList;

public record UpdateWishListCommand : RequestBase
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public VisabilityLevel VisabilityLevel { get; set; }
}
