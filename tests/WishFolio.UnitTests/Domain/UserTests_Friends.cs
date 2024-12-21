using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.UserAgregate.Friends;

namespace WishFolio.UnitTests.Domain;

public class UserTests_Friends
{
    [Fact]
    public void AddFriend_ShouldAddFriendship()
    {
        // Arrange
        var user1 = new User("user1@example.com", "Tester1", 40);
        var user2 = new User("user2@example.com", "Tester2", 40);

        // Act
        user1.AddFriend(user2);

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
        var user1 = new User("user1@example.com", "Tester1", 40);
        var user2 = new User("user2@example.com", "Tester2", 40);
        user1.AddFriend(user2);

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
        var user1 = new User("user1@example.com", "Tester1", 40);
        var user2 = new User("user2@example.com", "Tester2", 40);
        user1.AddFriend(user2);

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
        var user1 = new User("user1@example.com", "Tester1", 40);
        var user2 = new User("user2@example.com", "Tester2", 40);
        user1.AddFriend(user2);
        user2.AcceptFriendRequest(user1);
        
        // Act
        user2.RemoveFriend(user1);

        // Assert
        Assert.DoesNotContain(user1.Friends, f=>f.FriendId == user2.Id);
        Assert.DoesNotContain(user2.Friends, f=>f.FriendId == user1.Id);
    }
}
