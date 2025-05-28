using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Entities;
using Networkie.Persistence.Abstractions;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Admin.Departments.Commands.UpsertDepartment;

public class UpsertDepartmentCommandHandler(
    IDepartmentRepository departmentRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpsertDepartmentCommand, IResult<UpsertDepartmentCommandResult>>
{
    public async Task<IResult<UpsertDepartmentCommandResult>> Handle(UpsertDepartmentCommand command, CancellationToken cancellationToken)
    {
        if (command.Id.HasValue)
        {
            var department = await departmentRepository.GetByIdAsync(command.Id.Value, cancellationToken);
            if (department == null)
                return ErrorResult<UpsertDepartmentCommandResult, string>.Create("Bölüm bulunamadı.");
            
            department.Name = command.Name;
            departmentRepository.Update(department);
        }
        else
        {
            var departments = await departmentRepository.GetByContainsNameAsync(command.Name, cancellationToken);
            if (departments.Any())
                return ErrorResult<UpsertDepartmentCommandResult, string>.Create("Bölüm zaten mevcut.");
            
            await departmentRepository.AddAsync(new Department()
            {
                Name = command.Name
            }, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return SuccessResult<UpsertDepartmentCommandResult>.Create(new UpsertDepartmentCommandResult());
    }
}