using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Entities;
using Networkie.Persistence.Abstractions;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Admin.Universities.Commands.UpsertUniversity;

public class UpsertUniversityCommandHandler(
    IUniversityRepository universityRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpsertUniversityCommand, IResult<UpsertUniversityCommandResult>>
{
    public async Task<IResult<UpsertUniversityCommandResult>> Handle(UpsertUniversityCommand command, CancellationToken cancellationToken)
    {
        if (command.Id.HasValue)
        {
            var university = await universityRepository.GetByIdAsync(command.Id.Value, cancellationToken);
            if (university == null)
                return ErrorResult<UpsertUniversityCommandResult, string>.Create("Üniversite bulunamadı.");
            
            university.Name = command.Name;
            universityRepository.Update(university);
        }
        else
        {
            var universities = await universityRepository.GetByContainsNameAsync(command.Name, cancellationToken);
            if (universities.Any())
                return ErrorResult<UpsertUniversityCommandResult, string>.Create("Üniversite zaten mevcut.");
            
            await universityRepository.AddAsync(new University()
            {
                Name = command.Name
            }, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return SuccessResult<UpsertUniversityCommandResult>.Create(new UpsertUniversityCommandResult());
    }
}