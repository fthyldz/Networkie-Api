using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Admin.SocialPlatforms.Commands.UpsertSocialPlatform;

public record UpsertSocialPlatformCommand(Guid? Id, string Name, bool IsRequired) : ICommand<IResult<UpsertSocialPlatformCommandResult>>;

public record UpsertSocialPlatformCommandResult();