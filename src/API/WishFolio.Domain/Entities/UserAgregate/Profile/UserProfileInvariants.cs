namespace WishFolio.Domain.Entities.UserAgregate.Profile
{
    public static class UserProfileInvariants
    {
        public const int NameMinLenght = 1;
        public const int NameMaxLenght = 60;
        public const int MinAge = 14;
        public const int MaxAge = 120;
    }
}