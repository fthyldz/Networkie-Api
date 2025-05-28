using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Admin.Professions.Queries.Professions;

public record ProfessionsQuery(int PageIndex = 0, int PageSize = 25,
    string? Search = null) : IQuery<IResult<ProfessionsQueryResult>>;

public record ProfessionsQueryResult(IEnumerable<ProfessionsQueryDataResult> Data, long TotalCount);

public record ProfessionsQueryDataResult(
    Guid Id,
    string Name);