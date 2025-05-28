using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Admin.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserId) : ICommand<IResult<DeleteUserCommandResult>>;

public record DeleteUserCommandResult();
    