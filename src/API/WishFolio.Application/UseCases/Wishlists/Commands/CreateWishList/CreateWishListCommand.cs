using WishFolio.Application.Common;
using WishFolio.Domain.Entities.WishListAgregate;

namespace WishFolio.Application.UseCases.Wishlists.Commands.CreateWishList;

public record CreateWishListCommand(string Name, string Description, VisabilityLevel VisabilityLevel) : RequestBase;


