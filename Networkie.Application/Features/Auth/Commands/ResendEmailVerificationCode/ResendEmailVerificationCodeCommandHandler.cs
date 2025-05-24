using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Abstractions.Services;
using Networkie.Application.Common.Exceptions;
using Networkie.Application.Common.Extensions;
using Networkie.Application.Common.Results;
using Networkie.Entities;
using Networkie.Persistence.Abstractions;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Auth.Commands.ResendEmailVerificationCode;

public class ResendEmailVerificationCodeCommandHandler(
    IUserEmailVerificationRepository userEmailVerificationRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IEmailService emailService,
    IConfiguration configuration,
    IMemoryCache memoryCache
) : ICommandHandler<ResendEmailVerificationCodeCommand, IResult<ResendEmailVerificationCodeCommandResult>>
{
    public async Task<IResult<ResendEmailVerificationCodeCommandResult>> Handle(
        ResendEmailVerificationCodeCommand command, CancellationToken cancellationToken)
    {
        var userId = memoryCache.Get<Guid>(command.UniqueCode);

        var user = await userRepository.GetByIdAsync(userId, cancellationToken);

        if (user == null)
            throw new UnauthorizedException("User not found");

        var userEmailVerification = await userEmailVerificationRepository.GetByUserIdAsync(user.Id, cancellationToken);
        if (userEmailVerification != null)
        {
            userEmailVerificationRepository.Delete(userEmailVerification);
        }
        
        var newUserEmailVerification = new UserEmailVerification
        {
            UserId = user.Id,
            VerificationCode = Guid.NewGuid(),
            ExpiresAt = DateTime.Now.AddDays(7)
        };
        
        await userEmailVerificationRepository.AddAsync(newUserEmailVerification, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var domain = configuration.GetValue<string>("Domain")!;
        var subject = configuration.GetValue<string>("MailTemplates:EmailVerification:Subject")!;
        var content = configuration.GetValue<string>("MailTemplates:EmailVerification:Content")!
            .Replace("{{FullName}}", user.GetFullName())
            .Replace("{{Domain}}", domain)
            .Replace("{{EmailVerificationCode}}", newUserEmailVerification.VerificationCode.ToString());
        
        await emailService.SendEmailAsync(user.Email, subject, content);
        
        return SuccessResult<ResendEmailVerificationCodeCommandResult>.Create(new ResendEmailVerificationCodeCommandResult());
    }
}