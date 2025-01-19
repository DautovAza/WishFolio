namespace WishFolio.Application.UseCases.Wishlists.Dtos;

public record WishListItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
