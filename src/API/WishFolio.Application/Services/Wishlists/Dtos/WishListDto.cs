using WishFolio.Domain.Entities.WishListAgregate;

namespace WishFolio.Application.Services.Wishlists.Dtos;

public record WishListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public VisabilityLevel VisabilityLevel { get; set; }

    public List<WishListItemDto> Items { get; set; }
}
