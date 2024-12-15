using System.Text;
using System.Security.Cryptography;
using WishFolio.Domain.Abstractions.Auth;

namespace WishFolio.Infrastructure.Auth;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16; 
    private const int KeySize = 32; 
    private const int Iterations = 10000;

    public string HashPassword(string password)
    {
        using var rng = new RNGCryptoServiceProvider();
        var salt = new byte[SaltSize];
        rng.GetBytes(salt);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        var key = pbkdf2.GetBytes(KeySize);

        var hashParts = new StringBuilder();
        hashParts.Append(Iterations);
        hashParts.Append('.');
        hashParts.Append(Convert.ToBase64String(salt));
        hashParts.Append('.');
        hashParts.Append(Convert.ToBase64String(key));

        return hashParts.ToString();
    }

    public bool VerifyPassword(string hashedPassword, string password)
    {
        var parts = hashedPassword.Split('.');
        if (parts.Length != 3) return false;

        var iterations = int.Parse(parts[0]);
        var salt = Convert.FromBase64String(parts[1]);
        var key = Convert.FromBase64String(parts[2]);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        var keyToCheck = pbkdf2.GetBytes(KeySize);

        return keyToCheck.SequenceEqual(key);
    }
}
