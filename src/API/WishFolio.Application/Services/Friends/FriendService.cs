using AutoMapper;
using WishFolio.Application.Services.Friends.Dtos;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Entities.UserAgregate.Friends;

namespace WishFolio.Application.Services.Friends;

public class FriendService : IFriendService
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public FriendService(IUserRepository userRepository, ICurrentUserService currentUserService, IMapper mapper)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FriendDto>> GetFriendsAsync()
    {
        var userId = _currentUserService.GetCurrentUserId();

        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new KeyNotFoundException("Пользователь не найден.");
        }

        var friends = user.Friends.Where(f => f.Status == FriendshipStatus.Accepted).ToList();

        return friends
            .Select(_mapper.Map<Friendship, FriendDto>)
            .ToArray();
    }

    public async Task<IEnumerable<FriendDto>> GetIncommingFriendsInvitations()
    {
        var userId = _currentUserService.GetCurrentUserId();

        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new KeyNotFoundException("Пользователь не найден.");
        }

        var friends = user.Friends.Where(f => f.Status == FriendshipStatus.Requested).ToList();

        return friends
            .Select(_mapper.Map<Friendship, FriendDto>)
            .ToArray();
    }

    public async Task<IEnumerable<FriendDto>> GetSentFriendsInvitations()
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

    public async Task AcceptFriendRequest(Guid friendId)
    {
        var userId = _currentUserService.GetCurrentUserId();

        var user = await _userRepository.GetByIdAsync(userId);

        var friend = await _userRepository.GetByIdAsync(friendId);

        if (user == null || friend == null)
        {
            throw new KeyNotFoundException("Пользователь не найден.");
        }

        user.AcceptFriendRequest(friend);
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

    public async Task RejectFriendRequest(Guid friendId)
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
