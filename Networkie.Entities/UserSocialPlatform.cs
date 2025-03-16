using Networkie.Entities.Abstractions;

namespace Networkie.Entities;

public class UserSocialPlatform : IBaseEntity
{
    public Guid UserId { get; set; }
    public Guid SocialPlatformId { get; set; }
    public string Url { get; set; }
}