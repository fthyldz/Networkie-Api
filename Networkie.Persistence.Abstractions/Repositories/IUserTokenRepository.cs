using Networkie.Entities;

namespace Networkie.Persistence.Abstractions.Repositories;

public interface IUserTokenRepository : IRepository<UserToken>
{
    Task<UserToken?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<UserToken?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
}