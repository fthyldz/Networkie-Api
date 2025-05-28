using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Admin.Departments.Commands.DeleteDepartment;

public class DeleteDepartmentCommandHandler(
    IDepartmentRepository departmentRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteDepartmentCommand, IResult<DeleteDepartmentCommandResult>>
{
    public async Task<IResult<DeleteDepartmentCommandResult>> Handle(DeleteDepartmentCommand command, CancellationToken cancellationToken)
    {
        var department = await departmentRepository.GetByIdAsync(command.DepartmentId, cancellationToken);

        if (department == null)
            return ErrorResult<DeleteDepartmentCommandResult, string>.Create("Bölüm bulunamadı.");

        departmentRepository.Delete(department);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return SuccessResult<DeleteDepartmentCommandResult>.Create(new DeleteDepartmentCommandResult());
    }
}