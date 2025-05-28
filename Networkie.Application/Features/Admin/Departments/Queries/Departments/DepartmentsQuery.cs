using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Admin.Departments.Queries.Departments;

public record DepartmentsQuery(int PageIndex = 0, int PageSize = 25,
    string? Search = null) : IQuery<IResult<DepartmentsQueryResult>>;

public record DepartmentsQueryResult(IEnumerable<DepartmentsQueryDataResult> Data, long TotalCount);

public record DepartmentsQueryDataResult(
    Guid Id,
    string Name);