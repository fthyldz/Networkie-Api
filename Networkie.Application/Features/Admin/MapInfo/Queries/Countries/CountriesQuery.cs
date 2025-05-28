using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Admin.MapInfo.Queries.Countries;

public record CountriesQuery(int PageIndex = 0, int PageSize = 25,
    string? Search = null) : IQuery<IResult<CountriesQueryResult>>;

public record CountriesQueryResult(IEnumerable<CountriesQueryDataResult> Data, long TotalCount);

public record CountriesQueryDataResult(
    Guid Id,
    string Name);