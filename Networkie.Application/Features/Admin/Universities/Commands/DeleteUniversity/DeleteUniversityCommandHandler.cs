using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Admin.Universities.Commands.DeleteUniversity;

public class DeleteUniversityCommandHandler(
    IUniversityRepository universityRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteUniversityCommand, IResult<DeleteUniversityCommandResult>>
{
    public async Task<IResult<DeleteUniversityCommandResult>> Handle(DeleteUniversityCommand command, CancellationToken cancellationToken)
    {
        var university = await universityRepository.GetByIdAsync(command.UniversityId, cancellationToken);

        if (university == null)
            return ErrorResult<DeleteUniversityCommandResult, string>.Create("Üniversite bulunamadı.");

        universityRepository.Delete(university);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return SuccessResult<DeleteUniversityCommandResult>.Create(new DeleteUniversityCommandResult());
    }
}