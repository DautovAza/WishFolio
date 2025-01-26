namespace WishFolio.Application.UseCases.Wishlists.Queries.Dtos;

public record WishListItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
