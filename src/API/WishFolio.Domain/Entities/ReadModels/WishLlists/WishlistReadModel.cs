using WishFolio.Domain.Entities.WishListAgregate;

namespace WishFolio.Domain.Entities.ReadModels.WishLlists;

public class WishlistReadModel
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public VisabilityLevel Visibility { get; set; }
}
