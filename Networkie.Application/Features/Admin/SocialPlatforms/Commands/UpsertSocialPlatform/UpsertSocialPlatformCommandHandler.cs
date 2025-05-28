using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Entities;
using Networkie.Persistence.Abstractions;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Admin.SocialPlatforms.Commands.UpsertSocialPlatform;

public class UpsertSocialPlatformCommandHandler(
    ISocialPlatformRepository socialPlatformRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpsertSocialPlatformCommand, IResult<UpsertSocialPlatformCommandResult>>
{
    public async Task<IResult<UpsertSocialPlatformCommandResult>> Handle(UpsertSocialPlatformCommand command, CancellationToken cancellationToken)
    {
        if (command.Id.HasValue)
        {
            var socialPlatform = await socialPlatformRepository.GetByIdAsync(command.Id.Value, cancellationToken);
            if (socialPlatform == null)
                return ErrorResult<UpsertSocialPlatformCommandResult, string>.Create("Sosyal platform bulunamadı.");
            
            socialPlatform.Name = command.Name;
            socialPlatform.IsRequired = command.IsRequired;
            socialPlatformRepository.Update(socialPlatform);
        }
        else
        {
            var socialPlatforms = await socialPlatformRepository.GetByContainsNameAsync(command.Name, cancellationToken);
            if (socialPlatforms.Any())
                return ErrorResult<UpsertSocialPlatformCommandResult, string>.Create("Sosyal platform zaten mevcut.");
            
            await socialPlatformRepository.AddAsync(new SocialPlatform()
            {
                Name = command.Name,
                IsRequired = command.IsRequired
            }, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return SuccessResult<UpsertSocialPlatformCommandResult>.Create(new UpsertSocialPlatformCommandResult());
    }
}