namespace WishFolio.Domain.Entities.UserAgregate.Friends;

public class Friendship
{
    public Guid UserId { get; private set; }
    public Guid FriendId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public FriendshipStatus Status { get; private set; }

    public Friendship(Guid userId, Guid friendId)
    {
        UserId = userId;
        FriendId = friendId;
        CreatedAt = DateTime.UtcNow;
        Status = FriendshipStatus.Pending;
    }

    public void Accept()
    {
        if (Status < FriendshipStatus.Pending || Status > FriendshipStatus.Requested)
        {
            throw new InvalidOperationException("Нельзя подтвердить несуществующий запрос.");
        }

        Status = FriendshipStatus.Accepted;
    }

    public void Decline()
    {
        if (Status == FriendshipStatus.Declined)
        {
            throw new InvalidOperationException("Нельзя отклонить несуществующий запрос.");
        }

        Status = FriendshipStatus.Declined;
    }

    public static Friendship CreateFriendshipRequest(Guid userId, Guid friendId)
    {
        return new Friendship(userId, friendId)
        {
            Status = FriendshipStatus.Requested,
        };
    }
}
