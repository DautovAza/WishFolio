using Moq;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.UserAgregate.Friends;

namespace WishFolio.UnitTests.Domain;

public class UserTests_Friends
{
    private Mock<IPasswordHasher> _passwordHasherMoq;

    public UserTests_Friends()
    {
        _passwordHasherMoq = new Mock<IPasswordHasher>();
    }

    [Fact]
    public void AddFriend_ShouldAddFriendship()
    {
        // Arrange
        User user1 = User.Create("user1@example.com", "Tester1", 40,"Password",_passwordHasherMoq.Object);
        User user2 =  User.Create("user2@example.com", "Tester2", 40, "Password", _passwordHasherMoq.Object);

        // Act
        user1.AddToFriends(user2);

        // Assert
        Assert.Single(user1.Friends);
        Assert.Equal(user2.Id, user1.Friends.First().FriendId);
        Assert.Equal(FriendshipStatus.Pending, user1.Friends.First().Status);
        Assert.Equal(FriendshipStatus.Requested, user2.Friends.First().Status);
    }

    [Fact]
    public void AcceptFriendRequest_ShouldChangeStatusToAccepted()
    {
        // Arrange
        User user1 = User.Create("user1@example.com", "Tester1", 40, "Password", _passwordHasherMoq.Object);
        User user2 = User.Create("user2@example.com", "Tester2", 40, "Password", _passwordHasherMoq.Object);
        user1.AddToFriends(user2);

        // Act
        user2.AcceptFriendRequest(user1);

        // Assert
        Assert.Equal(FriendshipStatus.Accepted, user1.Friends.First().Status);
        Assert.Equal(FriendshipStatus.Accepted, user2.Friends.First().Status);
    }

    [Fact]
    public void DeslineFriendRequest_ShouldChangeStatusToDeslined()
    {
        // Arrange
        User user1 = User.Create("user1@example.com", "Tester1", 40, "Password", _passwordHasherMoq.Object);
        User user2 = User.Create("user2@example.com", "Tester2", 40, "Password", _passwordHasherMoq.Object);
        user1.AddToFriends(user2);

        // Act
        user2.RemoveFriend(user1);

        // Assert
        Assert.Equal(FriendshipStatus.Declined, user1.Friends.First().Status);
        Assert.Equal(FriendshipStatus.Declined, user2.Friends.First().Status);
    }

    [Fact]
    public void RemoveFriendRequest_ShouldChangeStatusToDeslined()
    {
        // Arrange
        User user1 = User.Create("user1@example.com", "Tester1", 40, "Password", _passwordHasherMoq.Object);
        User user2 = User.Create("user2@example.com", "Tester2", 40, "Password", _passwordHasherMoq.Object);
        user1.AddToFriends(user2);
        user2.AcceptFriendRequest(user1);
        
        // Act
        user2.RemoveFriend(user1);

        // Assert
        Assert.DoesNotContain(user1.Friends, f=>f.FriendId == user2.Id);
        Assert.DoesNotContain(user2.Friends, f=>f.FriendId == user1.Id);
    }
}
