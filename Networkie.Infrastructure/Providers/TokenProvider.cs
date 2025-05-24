using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Networkie.Application.Abstractions.Providers;
using Networkie.Application.Common.Extensions;
using Networkie.Application.Dtos.TokenProviderDtos;
using ICryptoProvider = Networkie.Application.Abstractions.Providers.ICryptoProvider;

namespace Networkie.Infrastructure.Providers;

public class TokenProvider(IConfiguration configuration, ICryptoProvider cryptoProvider) : ITokenProvider
{
    public TokenDto GenerateToken(GenerateTokenDto generateTokenRequestDto)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"]!));

        var dateTimeNow = DateTime.Now;
        var expiresAt = dateTimeNow.AddMinutes(configuration.GetValue("JWT:TokenExpiration", 30));

        var tokenPayload = new TokenPayloadDto(generateTokenRequestDto.UserId, generateTokenRequestDto.Email);
        var payloadJson = JsonSerializer.Serialize(tokenPayload);
        var encryptedPayload = cryptoProvider.Encrypt(payloadJson);

        var jwt = new JwtSecurityToken(
            claims:
            [
                new Claim(ClaimTypes.UserData, encryptedPayload),
                new Claim(ClaimTypes.NameIdentifier, generateTokenRequestDto.UserId.ToString()),
                new Claim(ClaimTypes.Role, JsonSerializer.Serialize(generateTokenRequestDto.Roles)),
                new Claim(ClaimTypes.Anonymous, generateTokenRequestDto.IsProfileCompleted.ToString().ToLower()),
                new Claim(ClaimTypes.Version, generateTokenRequestDto.IsEmailVerified.ToString().ToLower()),
            ],
            notBefore: dateTimeNow,
            expires: expiresAt,
            signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
        );

        return new TokenDto(new JwtSecurityTokenHandler().WriteToken(jwt), dateTimeNow, expiresAt);
    }

    public RefreshTokenDto GenerateRefreshToken()
    {
        var refreshToken = Guid.NewGuid().ToString().EncodeBase64();
        var createdAt = DateTime.Now;
        var expiresAt = createdAt.AddMinutes(configuration.GetValue("JWT:RefreshTokenExpiration", 40));

        return new RefreshTokenDto(refreshToken, createdAt, expiresAt);
    }
}