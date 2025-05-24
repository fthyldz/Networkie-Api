using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Auth.Commands.VerifyEmail;

public record VerifyEmailCommand(Guid EmailVerificationCode) : ICommand<IResult<VerifyEmailCommandResult>>;

public record VerifyEmailCommandResult();