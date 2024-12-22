using WishFolio.Application.Services.Friends.Dtos;

namespace WishFolio.Application.Services.Friends;

public interface IFriendService
{
    Task AddFriendAsync(Guid friendId);
    Task RemoveFriendAsync(Guid friendId);
    Task AcceptFriendRequest(Guid friendId);
    Task RejectFriendRequest(Guid friendId);
    Task<IEnumerable<FriendDto>> GetFriendsAsync();
    Task<IEnumerable<FriendDto>> GetIncommingFriendsInvitations();
    Task<IEnumerable<FriendDto>> GetSentFriendsInvitations();
}
