namespace WishFolio.WebApi.Controllers.Friends.ViewModels;

public class AddFriendRequest
{
    public Guid? FriendId { get; set; }
    public string? FriendEmail { get; set; }
}
