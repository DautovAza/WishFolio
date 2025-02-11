namespace WishFolio.WebApi.Controllers.Friends.ViewModels;

public record FriendModel
{
    public Guid Id { get; set; }
    public string Status { get; set; }
    public string Name { get; set; }
}