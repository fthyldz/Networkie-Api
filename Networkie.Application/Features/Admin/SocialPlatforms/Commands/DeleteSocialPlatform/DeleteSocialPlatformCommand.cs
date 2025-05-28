using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Admin.SocialPlatforms.Commands.DeleteSocialPlatform;

public record DeleteSocialPlatformCommand(Guid Id) : ICommand<IResult<DeleteSocialPlatformCommandResult>>;

public record DeleteSocialPlatformCommandResult();