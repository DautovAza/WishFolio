using Moq;
using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Application.Services.Friends;
using WishFolio.Domain.Entities.UserAgregate.Friends;
using AutoMapper;
using WishFolio.Application.Services.Friends.Dtos;

namespace WishFolio.UnitTests.Application;

public class FriendServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMoq;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly IMapper _mapper;


    public FriendServiceTests()
    {
        _passwordHasherMoq = new Mock<IPasswordHasher>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();
        _mapper = new MapperConfiguration(e => e.AddProfile(new FriendDtoMapping(_userRepositoryMock.Object)))
            .CreateMapper();

        _passwordHasherMoq.Setup(f => f.VerifyPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
    }

    [Fact]
    public async Task GetFriends_UserNotFound_ThrowsKeyNotFoundException()
    {

        var friendService = new FriendService(_userRepositoryMock.Object, _currentUserServiceMock.Object, _mapper);

        _userRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                           .ReturnsAsync((User)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => friendService.GetFriendsAsync());
    }

    [Fact]
    public async Task GetFriends_ReturnsOnlyAcceptedFriends()
    {
        // Arrange
        User user = CreateUser();        
        var friends = CreateFriendsForUser(user, 3);
        var friendsRequests = CreateFriendsRequestsFromUser(user, 2);

        var friendService = new FriendService(_userRepositoryMock.Object, _currentUserServiceMock.Object, _mapper);

        // Act
        var friendsResult = await friendService.GetFriendsAsync();
        //Assert

        Assert.Equal(3, friendsResult.Count());
        Assert.All(friendsResult, fr => Assert.Equal( FriendshipStatus.Accepted, fr.Status));
    } 
    
    [Fact]
    public async Task GetFriendsRequests_ReturnsOnlyRequestedFriends()
    {
        // Arrange
        User user = CreateUser();        
        var friends = CreateFriendsForUser(user, 3);
        var friendsRequests = CreateFriendsRequestsFromUser(user, 2);
        var friendsExtRequests = CreateExternalFriendsRequestsForUser(user, 4);

        var friendService = new FriendService(_userRepositoryMock.Object, _currentUserServiceMock.Object, _mapper);

        // Act
        var friendsResult = await friendService.GetIncommingFriendsInvitations();
        //Assert

        Assert.Equal(friendsExtRequests.Count(), friendsResult.Count());
        Assert.All(friendsResult, fr => Assert.Equal( FriendshipStatus.Requested, fr.Status));
    }    

    [Fact]
    public async Task GetFriendsSentRequests_ReturnsOnlyPendingFriends()
    {
        // Arrange
        User user = CreateUser();        
        var friends = CreateFriendsForUser(user, 3);
        var friendsRequests = CreateFriendsRequestsFromUser(user, 2);
        var friendsExtRequests = CreateExternalFriendsRequestsForUser(user, 4);

        var friendService = new FriendService(_userRepositoryMock.Object, _currentUserServiceMock.Object, _mapper);

        // Act
        var friendsResult = await friendService.GetSentFriendsInvitations();
        //Assert

        Assert.Equal(friendsRequests.Count(), friendsResult.Count());
        Assert.All(friendsResult, fr => Assert.Equal( FriendshipStatus.Pending, fr.Status));
    }

    private User CreateUser()
    {
        var user = new User("test@example.com", "Test User", 45);
        user.SetPassword("Password", _passwordHasherMoq.Object);
        SetupRepositoryForUser(user);

        _currentUserServiceMock.Setup(s => s.GetCurrentUserId())
            .Returns(user.Id);

        return user;
    }

    private IEnumerable<User> CreateFriendsForUser(User user, int count)
    {
        return Enumerable
            .Range(0, count)
            .Select(i =>
            {
                User friend = new User($"testFriend{i + 1}@example.com", $"Test friend {i + 1}", 45);

                SetupRepositoryForUser(friend);

                user.AddFriend(friend);
                friend.AcceptFriendRequest(user);
                return friend;
            })
            .ToArray();
    }
   
    private IEnumerable<User> CreateFriendsRequestsFromUser(User user, int count = 1)
    {
        return Enumerable
            .Range(0, count)
            .Select(i =>
            {
                User friend = new User($"testFriendRequest{i + 1}@example.com", $"Test friend request {i + 1}", 45);

                SetupRepositoryForUser(friend);

                user.AddFriend(friend);
                return friend;
            })
            .ToArray();
    }
    
    private IEnumerable<User> CreateExternalFriendsRequestsForUser(User user, int count = 1)
    {
        return Enumerable
            .Range(0, count)
            .Select(i =>
            {
                User friend = new User($"testFriendRequest{i + 1}@example.com", $"Test friend request {i + 1}", 45);

                SetupRepositoryForUser(friend);

                friend.AddFriend(user);
                return friend;
            })
            .ToArray();
    }

    private void SetupRepositoryForUser(User friend)
    {
        _userRepositoryMock.Setup(repo => repo.GetProfileByIdAsync(friend.Id))
            .ReturnsAsync(friend.Profile);

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(friend.Id))
            .ReturnsAsync(friend);
    }
}