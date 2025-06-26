using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace SocialNetwork.Infrastructure.Security;

public static class PasswordHashProvider
{
    private const int SaltSizeInBits = 128;
    private const int HashSizeInBits = 256;
    private const int IterationCount = 100_000;

    public static string GetSalt()
    {
        int saltSizeInBytes = SaltSizeInBits / 8;
        var salt = RandomNumberGenerator.GetBytes(saltSizeInBytes);
        return Convert.ToBase64String(salt);
    }

    public static string GetHash(string password, string base64Salt)
    {
        var salt = Convert.FromBase64String(base64Salt);
        int hashSizeInBytes = HashSizeInBits / 8;

        var hash = KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            iterationCount: IterationCount,
            numBytesRequested: hashSizeInBytes
        );

        return Convert.ToBase64String(hash);
    }
}

