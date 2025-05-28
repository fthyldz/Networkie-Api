using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Admin.SocialPlatforms.Commands.DeleteSocialPlatform;

public class DeleteSocialPlatformCommandHandler(
    ISocialPlatformRepository socialPlatformRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteSocialPlatformCommand, IResult<DeleteSocialPlatformCommandResult>>
{
    public async Task<IResult<DeleteSocialPlatformCommandResult>> Handle(DeleteSocialPlatformCommand command, CancellationToken cancellationToken)
    {
        var socialPlatform = await socialPlatformRepository.GetByIdAsync(command.Id, cancellationToken);

        if (socialPlatform == null)
            return ErrorResult<DeleteSocialPlatformCommandResult, string>.Create("Sosyal platform bulunamadı.");

        socialPlatformRepository.Delete(socialPlatform);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return SuccessResult<DeleteSocialPlatformCommandResult>.Create(new DeleteSocialPlatformCommandResult());
    }
}