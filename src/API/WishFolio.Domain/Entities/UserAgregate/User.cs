using WishFolio.Domain.Abstractions.Entities;
using WishFolio.Domain.Entities.UserAgregate.Friends;
using WishFolio.Domain.Entities.UserAgregate.Notifications;
using WishFolio.Domain.Entities.UserAgregate.Profile;
using WishFolio.Domain.Entities.UserAgregate.ValueObjects;

namespace WishFolio.Domain.Entities.UserAgregate;

public class User : AuditableEntity
{
    private readonly List<Friendship> _friends;
    private readonly List<Notification> _notifications;

    public Email Email { get; private set; }
    public string PasswordHash { get; private set; } // TODO: добавить хеширование пароля
    public UserProfile Profile { get; private set; }

    public IReadOnlyCollection<Friendship> Friends => _friends.AsReadOnly();
    public IReadOnlyList<Notification> Notifications => _notifications.AsReadOnly();

    private User() : base() { }

    public User(string email, string name, int age)
        : base(Guid.NewGuid(), DateTime.UtcNow)
    { 
        Email = new Email(email);
        Profile = new UserProfile(name, age);
        _friends = new List<Friendship>();
    }

    public void SetPassword(string password, IPasswordHasher hasher)
    {
        PasswordHash = hasher.HashPassword(password);
    }

    public bool ValidatePassword(string password, IPasswordHasher hasher)
    {
        return hasher.VerifyPassword(PasswordHash, password);
    }

    public void AddFriend(Guid friendId)
    {
        if (_friends.Exists(f => f.FriendId == friendId))
        {
            throw new InvalidOperationException("Друг уже добавлен.");
        }

        _friends.Add(new Friendship(Id, friendId));
    }

    public void RemoveFriend(Guid friendId)
    {
        var friend = _friends.Find(f => f.FriendId == friendId);
        if (friend == null)
        {
            throw new InvalidOperationException("Друг не найден.");
        }

        _friends.Remove(friend);
    }

    public void AcceptFriendRequest(User friendUser)
    {
        var friendship = _friends.Find(f => f.FriendId == friendUser.Id);
        if (friendship == null)
        {
            throw new InvalidOperationException("Запрос на дружбу не найден.");
        }

        friendship.Accept();
    }

    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
    }
}
