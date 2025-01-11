namespace WishFolio.Application.Services.Wishlists.Dtos;

public record WishListItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
