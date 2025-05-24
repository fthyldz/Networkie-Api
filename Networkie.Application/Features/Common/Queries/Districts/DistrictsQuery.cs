using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Common.Queries.Districts;

public record DistrictsQuery(Guid CityId) : IQuery<IResult<IEnumerable<DistrictsQueryResult>>>;

public record DistrictsQueryResult(Guid Id, string Name);