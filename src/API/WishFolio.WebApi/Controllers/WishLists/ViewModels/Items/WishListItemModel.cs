namespace WishFolio.WebApi.Controllers.WishLists.ViewModels.Items;

public record WishListItemModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
