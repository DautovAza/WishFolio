using WishFolio.Domain.Errors;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Domain.Entities.UserAgregate.Friends;

public class Friendship
{
    public Guid UserId { get; private set; }
    public Guid FriendId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public FriendshipStatus Status { get; private set; }

    private Friendship() { }

    private Friendship(Guid userId, Guid friendId)
    {
        UserId = userId;
        FriendId = friendId;
        CreatedAt = DateTime.UtcNow;
        Status = FriendshipStatus.Pending;
    }

    public Result Accept()
    {
        if (Status < FriendshipStatus.Pending || Status > FriendshipStatus.Requested)
        {
            return Result.Failure(DomainErrors.Friend.FriendRequestNotFound());
        }

        Status = FriendshipStatus.Accepted;
        return Result.Ok();
    }

    public Result Decline()
    {
        if (Status == FriendshipStatus.Declined)
        {
            return Result.Failure(DomainErrors.Friend.FriendRequestNotFound());
        }

        Status = FriendshipStatus.Declined;
        return Result.Ok();
    }

    public static Friendship CreateFriendshipRequest(Guid userId, Guid friendId)
    {
        return new Friendship(userId, friendId)
        {
            Status = FriendshipStatus.Requested,
        };
    }  
    
    public static Friendship CreateFriendshipSent(Guid userId, Guid friendId)
    {
        return new Friendship(userId, friendId)
        {
            Status = FriendshipStatus.Pending,
        };
    }
}
