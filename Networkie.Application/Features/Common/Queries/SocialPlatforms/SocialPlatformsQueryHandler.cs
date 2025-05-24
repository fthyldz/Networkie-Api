using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Common.Queries.SocialPlatforms;

public class SocialPlatformsQueryHandler(ISocialPlatformRepository socialPlatformRepository) : IQueryHandler<SocialPlatformsQuery, IResult<IEnumerable<SocialPlatformsQueryResult>>>
{
    public async Task<IResult<IEnumerable<SocialPlatformsQueryResult>>> Handle(SocialPlatformsQuery query, CancellationToken cancellationToken)
    {
        var socialPlatforms = await socialPlatformRepository.GetAllAsync(cancellationToken);
        return SuccessResult<IEnumerable<SocialPlatformsQueryResult>>.Create(socialPlatforms.Select(sp => new SocialPlatformsQueryResult(sp.Id, sp.Name, sp.IsRequired)));
    }
}