namespace WishFolio.WebApi.Controllers.Friends.ViewModels;

public record AddFriendModel
{
    public Guid? UserId { get; set; }
    public string? UserEmail { get; set; }
}
