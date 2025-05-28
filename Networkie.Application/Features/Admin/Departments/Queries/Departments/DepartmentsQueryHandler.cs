using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Admin.Departments.Queries.Departments;

public class DepartmentsQueryHandler(
    IDepartmentRepository departmentRepository)
    : IQueryHandler<DepartmentsQuery, IResult<DepartmentsQueryResult>>
{
    public async Task<IResult<DepartmentsQueryResult>> Handle(DepartmentsQuery query, CancellationToken cancellationToken)
    {
        
        var departments = await departmentRepository.GetDepartmentsAsPagedForAdmin(query.PageIndex, query.PageSize, query.Search,
            cancellationToken);
        var departmentsCount = await departmentRepository.GetDepartmentsAsPagedTotalCountForAdmin(query.Search, cancellationToken);

        var data = departments.Select(u => new DepartmentsQueryDataResult(
            u.Id,
            u.Name));

        var result = new DepartmentsQueryResult(data, departmentsCount);

        return SuccessResult<DepartmentsQueryResult>.Create(result);
    }
}