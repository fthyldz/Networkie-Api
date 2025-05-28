using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Admin.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteUserCommand, IResult<DeleteUserCommandResult>>
{
    public async Task<IResult<DeleteUserCommandResult>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);

        if (user == null)
            return ErrorResult<DeleteUserCommandResult, string>.Create("User not found");

        userRepository.Delete(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return SuccessResult<DeleteUserCommandResult>.Create(new DeleteUserCommandResult());
    }
}