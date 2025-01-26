using WishFolio.Domain;

namespace WishFolio.Application.UseCases.Wishlists.Queries.Dtos;

public record WishListItemDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Uri { get; set; }
    public ReservationStatus Status { get; set; }
    public string ReservedBy { get; set; }
}