using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Admin.SocialPlatforms.Queries.SocialPlatforms;

public class ProfessionsQueryHandler(
    ISocialPlatformRepository socialPlatformRepository)
    : IQueryHandler<SocialPlatformsQuery, IResult<SocialPlatformsQueryResult>>
{
    public async Task<IResult<SocialPlatformsQueryResult>> Handle(SocialPlatformsQuery query, CancellationToken cancellationToken)
    {
        var socialPlatforms = await socialPlatformRepository.GetSocialPlatformsAsPagedForAdmin(query.PageIndex, query.PageSize, query.Search,
            cancellationToken);
        var socialPlatformsCount = await socialPlatformRepository.GetSocialPlatformsAsPagedTotalCountForAdmin(query.Search, cancellationToken);

        var data = socialPlatforms.Select(sp => new SocialPlatformsQueryDataResult(
            sp.Id,
            sp.Name,
            sp.IsRequired));

        var result = new SocialPlatformsQueryResult(data, socialPlatformsCount);

        return SuccessResult<SocialPlatformsQueryResult>.Create(result);
    }
}