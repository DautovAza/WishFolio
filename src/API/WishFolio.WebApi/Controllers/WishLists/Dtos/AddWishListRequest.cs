using WishFolio.Domain.Entities.WishListAgregate;

namespace WishFolio.WebApi.Controllers.WishLists.Dtos;

public record AddWishListRequest(string Name,
    string Description, 
    VisabilityLevel VisabilityLevel);
