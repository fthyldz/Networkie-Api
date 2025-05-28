using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Admin.Professions.Commands.DeleteProfession;

public class DeleteProfessionCommandHandler(
    IProfessionRepository professionRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteProfessionCommand, IResult<DeleteProfessionCommandResult>>
{
    public async Task<IResult<DeleteProfessionCommandResult>> Handle(DeleteProfessionCommand command, CancellationToken cancellationToken)
    {
        var profession = await professionRepository.GetByIdAsync(command.Id, cancellationToken);

        if (profession == null)
            return ErrorResult<DeleteProfessionCommandResult, string>.Create("Meslek bulunamadı.");

        professionRepository.Delete(profession);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return SuccessResult<DeleteProfessionCommandResult>.Create(new DeleteProfessionCommandResult());
    }
}