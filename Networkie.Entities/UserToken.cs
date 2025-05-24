using Networkie.Entities.Abstractions;

namespace Networkie.Entities;

public class UserToken : IBaseEntity
{
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTime TokenCreatedAt { get; set; }
    public DateTime TokenExpiresAt { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenCreatedAt { get; set; }
    public DateTime RefreshTokenExpiresAt { get; set; }
}