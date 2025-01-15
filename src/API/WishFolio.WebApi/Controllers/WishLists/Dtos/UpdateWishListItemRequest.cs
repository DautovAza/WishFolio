namespace WishFolio.WebApi.Controllers.WishLists.Dtos;

public record UpdateWishListItemRequest(string Name,
    string Description, 
    string LinkUrl);
