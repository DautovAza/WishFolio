using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.UserAgregate.Friends;

namespace WishFolio.UnitTests.Domain;

public class UserTests_Friends 
{ 
    [Fact]
    public void AddFriend_ShouldAddFriendship()
    {
        // Arrange
        var user1 = new User("user1@example.com","Tester1", 40);
        var user2 = new User("user2@example.com","Tester2", 40);
       
        // Act
        user1.AddFriend(user2.Id);

        // Assert
        Assert.Single(user1.Friends);
        Assert.Equal(user2.Id, user1.Friends.First().FriendId);
        Assert.Equal(FriendshipStatus.Pending, user1.Friends.First().Status);
    }

    [Fact]
    public void AcceptFriendRequest_ShouldChangeStatus()
    {
        // Arrange
        var user1 = new User("user1@example.com", "Tester1",  40);
        var user2 = new User("user2@example.com", "Tester2",  40);
        user1.AddFriend(user2.Id);

        // Act
        user1.AcceptFriendRequest(user2);

        // Assert
        Assert.Equal(FriendshipStatus.Accepted, user1.Friends.First().Status);
    }
}
