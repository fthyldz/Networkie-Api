using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Common.Queries.SocialPlatforms;

public record SocialPlatformsQuery() : IQuery<IResult<IEnumerable<SocialPlatformsQueryResult>>>;

public record SocialPlatformsQueryResult(Guid Id, string Name, bool IsRequired);