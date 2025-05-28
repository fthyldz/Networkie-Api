using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Admin.Departments.Commands.UpsertDepartment;

public record UpsertDepartmentCommand(Guid? Id, string Name) : ICommand<IResult<UpsertDepartmentCommandResult>>;

public record UpsertDepartmentCommandResult();