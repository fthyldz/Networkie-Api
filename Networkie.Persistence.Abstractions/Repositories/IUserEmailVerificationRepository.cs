using Networkie.Entities;

namespace Networkie.Persistence.Abstractions.Repositories;

public interface IUserEmailVerificationRepository : IRepository<UserEmailVerification>
{
    Task<UserEmailVerification?> GetByVerificationCodeAsync(Guid verificationCode,
        CancellationToken cancellationToken = default);

    Task<UserEmailVerification?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}