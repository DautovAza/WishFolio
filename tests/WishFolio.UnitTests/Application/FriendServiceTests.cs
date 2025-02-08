using Moq;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Entities.UserAgregate.Friends;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Application.UseCases.Friends.Queries.Dtos;
using WishFolio.Application.UseCases.Friends.Commands.GetFriends;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Abstractions.Repositories.Write;

namespace WishFolio.UnitTests.Application;

public class FriendServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMoq;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly IMapper _mapper;


    public FriendServiceTests()
    {
        var services = new ServiceCollection();

        _passwordHasherMoq = new Mock<IPasswordHasher>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();

        services.AddSingleton(_userRepositoryMock.Object);
        services.AddTransient<ProfileNameResolver>();
        services.AddAutoMapper(cfg => cfg.AddProfile<FriendDtoMapping>());

        var serviceProvider = services.BuildServiceProvider();

        _mapper =serviceProvider.GetRequiredService<IMapper>();

        _passwordHasherMoq.Setup(f => f.VerifyPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
    }

    [Fact]
    public async Task GetFriends_UserNotFound_ThrowsKeyNotFoundException()
    {
        var friendService = new GetFriendsQueryHandler(_currentUserServiceMock.Object, _mapper);
        _userRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                           .ReturnsAsync((User)null);

        var result = await friendService.Handle(new GetFriendsQuery(FriendshipStatus.Accepted),default);

        Assert.True(result.IsFailure);
        Assert.Equal(DomainErrors.User.UserNotFound(), result.Errors.First());
    }

    [Fact]
    public async Task GetFriends_ReturnsOnlyAcceptedFriends()
    {
        // Arrange
        User user = CreateUser();        
        var friends = CreateFriendsForUser(user, 3);
        var friendsRequests = CreateFriendsRequestsFromUser(user, 2);

        var friendService = new GetFriendsQueryHandler(_currentUserServiceMock.Object, _mapper);

        // Act
        var friendsResult = await friendService.Handle(new GetFriendsQuery(FriendshipStatus.Accepted),default);
        //Assert

        Assert.Equal(3, friendsResult.Value.Count());
        Assert.All(friendsResult.Value, fr => Assert.Equal( FriendshipStatus.Accepted, fr.Status));
    } 
    
    [Fact]
    public async Task GetFriendsRequests_ReturnsOnlyRequestedFriends()
    {
        // Arrange
        User user = CreateUser();        
        var friends = CreateFriendsForUser(user, 3);
        var friendsRequests = CreateFriendsRequestsFromUser(user, 2);
        var friendsExtRequests = CreateExternalFriendsRequestsForUser(user, 4);

        var friendService = new GetFriendsQueryHandler(_currentUserServiceMock.Object, _mapper);

        // Act
        var friendsResult = await friendService.Handle(new GetFriendsQuery(FriendshipStatus.Requested),default);
        //Assert

        Assert.Equal(friendsExtRequests.Count(), friendsResult.Value.Count());
        Assert.All(friendsResult.Value, fr => Assert.Equal( FriendshipStatus.Requested, fr.Status));
    }    

    [Fact]
    public async Task GetFriendsSentRequests_ReturnsOnlyPendingFriends()
    {
        // Arrange
        User user = CreateUser();        
        var friends = CreateFriendsForUser(user, 3);
        var friendsRequests = CreateFriendsRequestsFromUser(user, 2);
        var friendsExtRequests = CreateExternalFriendsRequestsForUser(user, 4);

        var friendService = new GetFriendsQueryHandler(_currentUserServiceMock.Object, _mapper);

        // Act
        var friendsResult = await friendService.Handle(new GetFriendsQuery(FriendshipStatus.Pending),default);
        //Assert

        Assert.Equal(friendsRequests.Count(), friendsResult.Value.Count());
        Assert.All(friendsResult.Value, fr => Assert.Equal( FriendshipStatus.Pending, fr.Status));
    }

    private User CreateUser()
    {
        User user = User.Create ("test@example.com", "Test User", 45,"Password", _passwordHasherMoq.Object).Value;
        SetupRepositoryForUser(user);

        _currentUserServiceMock.Setup(s => s.GetCurrentUserId())
            .Returns(user.Id);   
        
        _currentUserServiceMock.Setup(s => s.GetCurrentUserAsync())
            .ReturnsAsync(user);

        return user;
    }

    private IEnumerable<User> CreateFriendsForUser(User user, int count)
    {
        return Enumerable
            .Range(0, count)
            .Select(i =>
            {
                User friend = User.Create($"testFriend{i + 1}@example.com", $"Test friend {i + 1}", 45, "Password", _passwordHasherMoq.Object);

                SetupRepositoryForUser(friend);

                user.AddToFriends(friend);
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
                User friend = User.Create($"testFriendRequest{i + 1}@example.com", $"Test friend request {i + 1}", 45, "Password", _passwordHasherMoq.Object);

                SetupRepositoryForUser(friend);

                user.AddToFriends(friend);
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
                User friend = User.Create ($"testFriendRequest{i + 1}@example.com", $"Test friend request {i + 1}", 45, "Password", _passwordHasherMoq.Object);

                SetupRepositoryForUser(friend);

                friend.AddToFriends(user);
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