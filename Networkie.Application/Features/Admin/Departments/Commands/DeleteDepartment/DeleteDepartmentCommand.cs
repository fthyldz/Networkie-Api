using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Admin.Departments.Commands.DeleteDepartment;

public record DeleteDepartmentCommand(Guid DepartmentId) : ICommand<IResult<DeleteDepartmentCommandResult>>;

public record DeleteDepartmentCommandResult();