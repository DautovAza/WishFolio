namespace WishFolio.WebApi.Controllers.WishLists.Dtos;

public record AddWishListItemRequest(string Name,
    string Description, 
    string LinkUrl);
