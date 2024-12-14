namespace WishFolio.Domain.Abstractions.Entities;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string password);
}
