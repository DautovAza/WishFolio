using WishFolio.Application.Services.Friends.Dtos;

namespace WishFolio.Application.Services.Friends
{
    public interface IFriendService
    {
        Task AddFriendAsync(Guid friendId);
        Task<IEnumerable<FriendDto>> GetFriendRequests();
        Task<IEnumerable<FriendDto>> GetFriends();
        Task<IEnumerable<FriendDto>> GetSentFriendRequests();
        Task RemoveFriendAsync(Guid friendId);
    }
}