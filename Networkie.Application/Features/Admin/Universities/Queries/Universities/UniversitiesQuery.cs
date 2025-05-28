using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Admin.Universities.Queries.Universities;

public record UniversitiesQuery(int PageIndex = 0, int PageSize = 25,
    string? Search = null) : IQuery<IResult<UniversitiesQueryResult>>;

public record UniversitiesQueryResult(IEnumerable<UniversitiesQueryDataResult> Data, long TotalCount);

public record UniversitiesQueryDataResult(
    Guid Id,
    string Name);