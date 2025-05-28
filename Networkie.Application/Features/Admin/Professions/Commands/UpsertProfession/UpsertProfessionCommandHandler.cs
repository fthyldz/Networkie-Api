using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Entities;
using Networkie.Persistence.Abstractions;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Admin.Professions.Commands.UpsertProfession;

public class UpsertProfessionCommandHandler(
    IProfessionRepository professionRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpsertProfessionCommand, IResult<UpsertProfessionCommandResult>>
{
    public async Task<IResult<UpsertProfessionCommandResult>> Handle(UpsertProfessionCommand command, CancellationToken cancellationToken)
    {
        if (command.Id.HasValue)
        {
            var profession = await professionRepository.GetByIdAsync(command.Id.Value, cancellationToken);
            if (profession == null)
                return ErrorResult<UpsertProfessionCommandResult, string>.Create("Meslek bulunamadı.");
            
            profession.Name = command.Name;
            professionRepository.Update(profession);
        }
        else
        {
            var professions = await professionRepository.GetByContainsNameAsync(command.Name, cancellationToken);
            if (professions.Any())
                return ErrorResult<UpsertProfessionCommandResult, string>.Create("Meslek zaten mevcut.");
            
            await professionRepository.AddAsync(new Profession()
            {
                Name = command.Name
            }, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return SuccessResult<UpsertProfessionCommandResult>.Create(new UpsertProfessionCommandResult());
    }
}