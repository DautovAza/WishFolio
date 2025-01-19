using WishFolio.Domain.Entities.UserAgregate.Friends;

namespace WishFolio.Domain.Errors;

public static partial class DomainErrors
{
    public static class Friend
    {
        public static Error FriendAlreadyExist(string friendName) => new(nameof(Friend),
            $"Нельзя добавить пользователя {friendName} в друзья, т.к. он уже был добавлен в друзья ранее.");
        
        public static Error IsNotFriend(string friendName) => new(nameof(Friend),
            $"Пользователь {friendName} не является вашим другом.");   
        
        public static Error FriendRequestNotFound() => new(nameof(Friendship),
            $"Запрос в друзья не был найден.");

        public static Error CannotAddSelfToFriends() => new(nameof(Friendship),
            $"Нельзя добавить себя в друзья.");
    }
}
