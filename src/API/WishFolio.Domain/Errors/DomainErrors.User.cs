using WishFolio.Domain.Entities.UserAgregate.Profile;
using WishFolio.Domain.Entities.UserAgregate.ValueObjects;

namespace WishFolio.Domain.Errors;

public static partial class DomainErrors
{
    public static class User
    {
        public static Error EmailInvalidFormat(string email) => new(nameof(Email),
            $"Неверный формат емейла ({email}). Емайл должен иметь формат user@example.com.");

        public static Error EmailIsNull() => new(nameof(Email),
            $"Емейл не может быть NULL.");

        public static Error UserProfileNameIsNullOrEmpty() => new(nameof(UserProfile.Name),
            $"Имя пользователя не может быть NULL или пустым.");

        public static Error UserProfileNameLenghtOutOfRange(string name) => new(nameof(UserProfile.Name),
            $"Длина имени пользователя ({name}) должна быть в диапазоне [{UserProfileInvariants.NameMinLenght}: {UserProfileInvariants.NameMaxLenght}].");

        public static Error UserProfileInvalidAge(int age) => new(nameof(UserProfile.Age),
            $"Возраст пользователя ({age}) должен находится в диапазоне [{UserProfileInvariants.MinAge}:{UserProfileInvariants.MaxAge}].");

        public static Error PasswordIsNullOrEmpty() => new("password",
            $"Пароль не может быть NULL или пустым.");

        public static Error UserWithSameEmailAlreadyExisted() => new(nameof(Email),
            $"Пользователь с таким email уже существует.");

        public static Error UserNotFound() => new(nameof(User),
            $"Пользователь не найден.");

        public static Error InvalidAuthorization() => new(nameof(User),
            $"Неверный email или пароль.");
    }
}
