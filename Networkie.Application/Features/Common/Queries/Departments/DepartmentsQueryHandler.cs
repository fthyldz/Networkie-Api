using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Common.Queries.Departments;

public class DepartmentsQueryHandler(IDepartmentRepository departmentRepository) : IQueryHandler<DepartmentsQuery, IResult<IEnumerable<DepartmentsQueryResult>>>
{
    public async Task<IResult<IEnumerable<DepartmentsQueryResult>>> Handle(DepartmentsQuery query, CancellationToken cancellationToken)
    {
        var departments = await departmentRepository.GetAllAsync(cancellationToken);
        return SuccessResult<IEnumerable<DepartmentsQueryResult>>.Create(departments.Select(d => new DepartmentsQueryResult(d.Id, d.Name)));
    }
}