using WishFolio.Application.Services.Friends.Dtos;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Entities.UserAgregate.Friends;

namespace WishFolio.Application.Services.Friends;

public class FriendService : IFriendService
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public FriendService(IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<FriendDto>> GetFriends()
    {
        var userId = _currentUserService.GetCurrentUserId();

        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new KeyNotFoundException("Пользователь не найден.");
        }

        var friends = user.Friends.Where(f => f.Status == FriendshipStatus.Accepted).ToList();

        return friends.Select(f => new FriendDto { Id = f.FriendId, Status = f.Status }).ToList();
    }

    public async Task<IEnumerable<FriendDto>> GetFriendRequests()
    {
        var userId = _currentUserService.GetCurrentUserId();

        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new KeyNotFoundException("Пользователь не найден.");
        }

        var friends = user.Friends.Where(f => f.Status == FriendshipStatus.Requested).ToList();

        return friends.Select(f => new FriendDto { Id = f.FriendId, Status = f.Status }).ToList();
    }

    public async Task<IEnumerable<FriendDto>> GetSentFriendRequests()
    {
        var userId = _currentUserService.GetCurrentUserId();

        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new KeyNotFoundException("Пользователь не найден.");
        }

        var friends = user.Friends.Where(f => f.Status == FriendshipStatus.Pending).ToList();

        return friends.Select(f => new FriendDto { Id = f.FriendId, Status = f.Status }).ToList();
    }

    public async Task AddFriendAsync(Guid friendId)
    {
        var userId = _currentUserService.GetCurrentUserId();

        if (userId == friendId)
        {
            throw new InvalidOperationException("Нельзя добавить себя в друзья.");
        }

        var user = await _userRepository.GetByIdAsync(userId);
        var friend = await _userRepository.GetByIdAsync(friendId);

        if (user == null || friend == null)
        {
            throw new KeyNotFoundException("Пользователь не найден.");
        }

        user.AddFriend(friend);
        await _userRepository.SaveChangesAsync();
    }

    public async Task RemoveFriendAsync(Guid friendId)
    {
        var userId = _currentUserService.GetCurrentUserId();

        var user = await _userRepository.GetByIdAsync(userId);
        var friend = await _userRepository.GetByIdAsync(friendId);
        if (user == null || friend == null)
        {
            throw new KeyNotFoundException("Пользователь не найден.");
        }

        user.RemoveFriend(friend);
        await _userRepository.SaveChangesAsync();
    }
}
