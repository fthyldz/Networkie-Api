using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Auth.Commands.ResendEmailVerificationCode;

public record ResendEmailVerificationCodeCommand(Guid UniqueCode): ICommand<IResult<ResendEmailVerificationCodeCommandResult>>;

public record ResendEmailVerificationCodeCommandResult();