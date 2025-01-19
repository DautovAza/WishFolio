using WishFolio.Domain.Errors;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Entities;
using WishFolio.Domain.Entities.WishListAgregate;
using WishFolio.Domain.Entities.UserAgregate.Friends;
using WishFolio.Domain.Entities.UserAgregate.Profile;
using WishFolio.Domain.Entities.UserAgregate.Notifications;
using WishFolio.Domain.Entities.UserAgregate.ValueObjects;
using WishFolio.Domain.Shared.ResultPattern;

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

#pragma warning disable CS8618 
    private User() : base() { }

    private User(Email email, UserProfile profile)
        : base(Guid.NewGuid(), DateTime.UtcNow)
#pragma warning restore CS8618 
    {
        Email = email;
        Profile = profile;

        _friends = new List<Friendship>();
        _notifications = new List<Notification>();
    }

    public static Result<User> Create(string email, string name, int age, string password, IPasswordHasher passwordHasher)
    {
        var emailResult = Email.Create(email);
        var profileResult = UserProfile.Create(name, age);

        var user = new User(emailResult.Value, profileResult.Value);

        user.SetPassword(password, passwordHasher);

        return Result<User>.Combine(user, [emailResult, profileResult]);
    }

    public Result SetPassword(string password, IPasswordHasher hasher)
    {
        if (string.IsNullOrEmpty(password))
        {
            return Result.Failure(DomainErrors.User.PasswordIsNullOrEmpty());
        }

        PasswordHash = hasher.HashPassword(password);
        return Result.Ok();
    }

    public bool ValidatePassword(string password, IPasswordHasher hasher)
    {
        return hasher.VerifyPassword(PasswordHash, password);
    }

    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
    }

    public Result AddToFriends(User friendUser)
    {
        if (_friends.Exists(f => f.FriendId == friendUser.Id))
        {
            return Result.Failure(DomainErrors.Friend.FriendAlreadyExist(friendUser.Profile.Name));
        }

        _friends.Add(Friendship.CreateFriendshipSent(Id, friendUser.Id));
        friendUser.AddFriendshipRequest(this);

        return Result.Ok();
    }

    public Result RemoveFriend(User friend)
    {
        var innerFriendship = FindFriendshipWithUser(friend.Id);
        var outerFriendship = friend.FindFriendshipWithUser(Id);

        if (innerFriendship is null || outerFriendship is null)
        {
            return Result.Failure(DomainErrors.Friend.IsNotFriend(friend.Profile.Name));
        }

        if (outerFriendship.Status == FriendshipStatus.Accepted && innerFriendship.Status == FriendshipStatus.Accepted)
        {
            _friends.Remove(innerFriendship);
            friend._friends.Remove(outerFriendship);
            return Result.Ok();
        }

        var friendshipDeclineResult = Result.Combine(
        [
            innerFriendship.Decline(),
            outerFriendship.Decline()
        ]);

        if (friendshipDeclineResult.IsSuccess)
        {
            friend.AddNotification(new Notification(friend.Id, NotificationType.FriendshipDeslined, $"Ваш запрос в друзья был отклонен пользователем {Profile.Name}"));
        }

        return friendshipDeclineResult;
    }

    public Result AcceptFriendRequest(User friend)
    {
        var innerFriendship = FindFriendshipWithUser(friend.Id);
        var outerFriendship = friend.FindFriendshipWithUser(Id);

        if (innerFriendship is null || outerFriendship is null)
        {
            return Result.Failure(DomainErrors.Friend.IsNotFriend(friend.Profile.Name));
        }

        var friendshipAcceptResult = Result.Combine(
        [
            innerFriendship.Accept(),
            outerFriendship.Accept()
        ]);

        if (friendshipAcceptResult.IsSuccess)
        {
            friend.AddNotification(new Notification(friend.Id, NotificationType.FriendshipAccepted, $"Ваш запрос в друзья был одобрен пользователем {Profile.Name}"));
        }

        return friendshipAcceptResult;
    }

    public VisabilityLevel GetWihListVisabilityLevelForUser(Guid otherUserId)
    {
        if (otherUserId == Id)
        {
            return VisabilityLevel.Private;
        }
        if (_friends.Any(f => f.FriendId == otherUserId))
        {
            return VisabilityLevel.FriendsOnly;
        }
        return VisabilityLevel.Public;

    }

    private Friendship? FindFriendshipWithUser(Guid friendId)
    {
        return _friends.FirstOrDefault(f => f.FriendId == friendId);
    }

    private void AddFriendshipRequest(User friend)
    {
        _friends.Add(Friendship.CreateFriendshipRequest(Id, friend.Id));
        AddNotification(new Notification(Id, NotificationType.FriendshipRequested, $"Запрос в друзья от пользователя {friend.Profile.Name}"));
    }
}
