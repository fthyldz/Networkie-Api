using Networkie.Entities.Abstractions;

namespace Networkie.Entities;

public class UserEmailVerification : IBaseEntity
{
    public Guid UserId { get; set; }
    public Guid VerificationCode { get; set; }
    public DateTime ExpiresAt { get; set; }
}