using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Networkie.Application.Abstractions.Providers;
using Networkie.Application.Dtos.TokenProviderDtos;

namespace Networkie.Infrastructure.Providers;

public class TokenProvider(IConfiguration configuration) : ITokenProvider
{
    public TokenDto GenerateTokenAsync(GenerateTokenDto generateTokenRequestDto, CancellationToken cancellationToken = default)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]!));
        
        var dateTimeNow = DateTime.UtcNow;
        var accessTokenExpiration = Convert.ToDouble(configuration["Jwt:AccessTokenExpiration"]);
        
        var jwt = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims:
            [
                new Claim(ClaimTypes.Email, generateTokenRequestDto.Email ?? ""),
                new Claim(ClaimTypes.MobilePhone, generateTokenRequestDto.PhoneNumber ?? ""),
                new Claim(ClaimTypes.NameIdentifier, generateTokenRequestDto.UserId),
            ],
            notBefore: dateTimeNow,
            expires: dateTimeNow.Add(TimeSpan.FromMinutes(accessTokenExpiration)),
            signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
        );

        return new TokenDto(new JwtSecurityTokenHandler().WriteToken(jwt));
    }
}