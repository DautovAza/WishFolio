namespace WishFolio.Domain.Abstractions.Auth;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string password);
}
