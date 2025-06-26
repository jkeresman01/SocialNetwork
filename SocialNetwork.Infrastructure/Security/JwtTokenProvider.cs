using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialNetwork.Infrastructure.Security;

public static class JwtTokenProvider
{
    private const int MinimumKeySizeInBytes = 32;

    public static string CreateToken(string secretKey, int expirationMinutes, string subject)
    {
        var tokenKey = Encoding.UTF8.GetBytes(secretKey);

        if (tokenKey.Length < MinimumKeySizeInBytes)
        {
            throw new ArgumentOutOfRangeException(nameof(secretKey),
                $"The secret key must be at least {MinimumKeySizeInBytes * 8} bits (i.e., {MinimumKeySizeInBytes} characters when UTF-8 encoded).");
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, subject),
                new Claim(JwtRegisteredClaimNames.Sub, subject)
            }),
            Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

