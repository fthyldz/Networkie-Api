using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Common.Queries.Cities;

public record CitiesQuery(Guid CountryId, Guid? StateId) : IQuery<IResult<IEnumerable<CitiesQueryResult>>>;

public record CitiesQueryResult(Guid Id, string Name);