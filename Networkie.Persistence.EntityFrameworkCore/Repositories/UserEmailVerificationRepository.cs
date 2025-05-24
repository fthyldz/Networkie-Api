using Microsoft.EntityFrameworkCore;
using Networkie.Entities;
using Networkie.Persistence.Abstractions.Repositories;
using Networkie.Persistence.EntityFrameworkCore.Contexts;

namespace Networkie.Persistence.EntityFrameworkCore.Repositories;

public class UserEmailVerificationRepository(IEfCoreDbContext context)
    : Repository<UserEmailVerification>(context), IUserEmailVerificationRepository
{
    public async Task<UserEmailVerification?> GetByVerificationCodeAsync(Guid verificationCode,
        CancellationToken cancellationToken = default)
        => await TableAsNoTracking.Where(
                u => u.VerificationCode == verificationCode && DateTime.UtcNow < u.ExpiresAt).OrderBy(u => u.ExpiresAt)
            .LastOrDefaultAsync(cancellationToken);

    public async Task<UserEmailVerification?> GetByUserIdAsync(Guid userId,
        CancellationToken cancellationToken = default)
        => await Table.Where(u => u.UserId == userId).OrderBy(u => u.ExpiresAt).LastOrDefaultAsync(cancellationToken);
}