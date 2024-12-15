using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Entities;
using WishFolio.Domain.Entities.UserAgregate.Friends;
using WishFolio.Domain.Entities.UserAgregate.Profile;
using WishFolio.Domain.Entities.UserAgregate.Notifications;
using WishFolio.Domain.Entities.UserAgregate.ValueObjects;

namespace WishFolio.Domain.Entities.UserAgregate;

public class User : AuditableEntity
{
    private readonly List<Friendship> _friends ;
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
        _notifications = new List<Notification>();
    }

    public void SetPassword(string password, IPasswordHasher hasher)
    {
        PasswordHash = hasher.HashPassword(password);
    }

    public bool ValidatePassword(string password, IPasswordHasher hasher)
    {
        return hasher.VerifyPassword(PasswordHash, password);
    }

    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
    }

    public void AddFriend(User friend)
    {
        if (_friends.Exists(f => f.FriendId == friend.Id))
        {
            throw new InvalidOperationException("Друг уже добавлен.");
        }

        _friends.Add(new Friendship(Id, friend.Id));
        friend.AddFriendshipRequest(this);
    }

    public void RemoveFriend(User friend)
    {
        var innerFriendship = FindFriendshipWithUser(friend.Id);
        var outerFriendship = friend.FindFriendshipWithUser(Id);

        if (outerFriendship.Status == FriendshipStatus.Accepted && innerFriendship.Status == FriendshipStatus.Accepted)
        {
            _friends.Remove(innerFriendship);
            friend._friends.Remove(outerFriendship);
        }
        else
        {
            innerFriendship.Decline();
            outerFriendship.Decline();
            friend.AddNotification(new Notification(friend.Id, NotificationType.FriendshipDeslined, $"Ваш запрос в друзья был отклонен пользователем {Profile.Name}"));
        }
    }

    public void AcceptFriendRequest(User friend)
    {
        var innerFriendship = FindFriendshipWithUser(friend.Id);
        var outerFriendship = friend.FindFriendshipWithUser(Id);

        innerFriendship.Accept();
        outerFriendship.Accept();

        friend.AddNotification(new Notification(friend.Id, NotificationType.FriendshipAccepted, $"Ваш запрос в друзья был одобрен пользователем {Profile.Name}"));
    }

    private Friendship FindFriendshipWithUser(Guid friendId)
    {
        var friendship = _friends.Find(f => f.FriendId == friendId);
        if (friendship == null)
        {
            throw new InvalidOperationException("Запрос на дружбу не найден.");
        }
        return friendship;
    }

    private void AddFriendshipRequest(User friend)
    {
        _friends.Add(Friendship.CreateFriendshipRequest(Id, friend.Id));
        AddNotification(new Notification(Id, NotificationType.FriendshipRequested, $"Запрос в друзья от пользователя {friend.Profile.Name}"));
    }
}
