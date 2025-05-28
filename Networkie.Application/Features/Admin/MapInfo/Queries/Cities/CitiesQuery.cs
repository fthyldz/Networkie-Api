using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Admin.MapInfo.Queries.Cities;

public record CitiesQuery(int PageIndex = 0, int PageSize = 25,
    string? SearchCity = null, string? SearchCountry = null) : IQuery<IResult<CitiesQueryResult>>;

public record CitiesQueryResult(IEnumerable<CitiesQueryDataResult> Data, long TotalCount);

public record CitiesQueryDataResult(
    Guid Id,
    string Name,
    Guid CountryId,
    string Country);