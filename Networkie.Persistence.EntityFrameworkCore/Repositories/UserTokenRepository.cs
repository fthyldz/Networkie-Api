using Microsoft.EntityFrameworkCore;
using Networkie.Entities;
using Networkie.Persistence.Abstractions.Repositories;
using Networkie.Persistence.EntityFrameworkCore.Contexts;

namespace Networkie.Persistence.EntityFrameworkCore.Repositories;

public class UserTokenRepository(IEfCoreDbContext context) : Repository<UserToken>(context), IUserTokenRepository
{
    public async Task<UserToken?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        => await Table.FirstOrDefaultAsync(ut => ut.UserId == userId, cancellationToken);
    
    public async Task<UserToken?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        => await Table.FirstOrDefaultAsync(ut => ut.RefreshToken == refreshToken && DateTime.Now < ut.RefreshTokenExpiresAt, cancellationToken);
}