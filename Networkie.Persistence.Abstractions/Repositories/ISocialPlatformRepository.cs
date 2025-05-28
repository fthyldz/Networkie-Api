using Networkie.Entities;

namespace Networkie.Persistence.Abstractions.Repositories;

public interface ISocialPlatformRepository : IRepository<SocialPlatform>
{
    Task<IEnumerable<SocialPlatform>> GetByContainsNameAsync(string name,
        CancellationToken cancellationToken = default);
    
    Task<IEnumerable<SocialPlatform>> GetSocialPlatformsAsPagedForAdmin(int pageIndex = 0, int pageSize = 25,
        string? search = null, CancellationToken cancellationToken = default);

    Task<long> GetSocialPlatformsAsPagedTotalCountForAdmin(string? search = null,
        CancellationToken cancellationToken = default);
}