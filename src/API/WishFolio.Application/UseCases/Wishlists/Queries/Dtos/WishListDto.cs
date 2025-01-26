using WishFolio.Domain.Entities.WishListAgregate;

namespace WishFolio.Application.UseCases.Wishlists.Queries.Dtos;

public record WishListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public VisabilityLevel VisabilityLevel { get; set; }

    public List<WishListItemDto> Items { get; set; }
}
