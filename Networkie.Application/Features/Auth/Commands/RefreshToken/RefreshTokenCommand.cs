using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : ICommand<IResult<RefreshTokenCommandResult>>;

public record RefreshTokenCommandResult(string Token, string RefreshToken);