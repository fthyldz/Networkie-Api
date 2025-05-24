using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Auth.Commands.VerifyEmail;

public class VerifyEmailCommandHandler(
    IUserEmailVerificationRepository userEmailVerificationRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
    ) : ICommandHandler<VerifyEmailCommand, IResult<VerifyEmailCommandResult>>
{
    public async Task<IResult<VerifyEmailCommandResult>> Handle(VerifyEmailCommand command,
        CancellationToken cancellationToken)
    {
        var verification =
            await userEmailVerificationRepository.GetByVerificationCodeAsync(command.EmailVerificationCode,
                cancellationToken);

        if (verification == null)
            return ErrorResult<VerifyEmailCommandResult, string>.Create("Geçersiz doğrulama kodu.");

        var user = await userRepository.GetByIdAsync(verification.UserId, cancellationToken);
        if (user == null)
            return ErrorResult<VerifyEmailCommandResult, string>.Create("Kullanıcı bulunamadı.");

        if (user.IsEmailVerified != true)
        {
            user.IsEmailVerified = true;
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return SuccessResult<VerifyEmailCommandResult>.Create(new VerifyEmailCommandResult());
    }
}