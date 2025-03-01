using WishFolio.WebApi.Controllers.WishLists.ViewModels.Items;

namespace WishFolio.WebApi.Controllers.WishLists.ViewModels.WishLists;

public record WishListModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string VisabilityLevel { get; set; }
}