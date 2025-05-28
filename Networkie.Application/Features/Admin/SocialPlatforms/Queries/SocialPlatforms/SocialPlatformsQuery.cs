using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Admin.SocialPlatforms.Queries.SocialPlatforms;

public record SocialPlatformsQuery(int PageIndex = 0, int PageSize = 25,
    string? Search = null) : IQuery<IResult<SocialPlatformsQueryResult>>;

public record SocialPlatformsQueryResult(IEnumerable<SocialPlatformsQueryDataResult> Data, long TotalCount);

public record SocialPlatformsQueryDataResult(
    Guid Id,
    string Name,
    bool IsRequired);