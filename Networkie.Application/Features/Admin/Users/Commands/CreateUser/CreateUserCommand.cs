using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Admin.Users.Commands.CreateUser;

public record CreateUserCommand(string Email, string FirstName, string? MiddleName, string? LastName, string Password, string Role) : ICommand<IResult<CreateUserCommandResult>>;

public record CreateUserCommandResult(Guid UniqueCode);
