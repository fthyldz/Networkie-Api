using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Common.Queries.Departments;

public record DepartmentsQuery() : IQuery<IResult<IEnumerable<DepartmentsQueryResult>>>;

public record DepartmentsQueryResult(Guid Id, string Name);