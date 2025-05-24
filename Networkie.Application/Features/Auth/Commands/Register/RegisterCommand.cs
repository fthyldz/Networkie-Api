using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Auth.Commands.Register;

public record RegisterCommand(string Email, string FirstName, string? MiddleName, string? LastName, string Password) : ICommand<IResult<RegisterCommandResult>>;

public record RegisterCommandResult(Guid UniqueCode);
