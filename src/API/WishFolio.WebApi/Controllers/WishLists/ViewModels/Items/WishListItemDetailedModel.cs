namespace WishFolio.WebApi.Controllers.WishLists.ViewModels.Items;

public record WishListItemDetailedModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Uri { get; set; }
    public string Status { get; set; }
    public string ReservedBy { get; set; }
}
