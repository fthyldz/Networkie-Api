using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Providers;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Abstractions.Services;
using Networkie.Application.Common.Extensions;
using Networkie.Application.Common.Results;
using Networkie.Entities;
using Networkie.Persistence.Abstractions;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Admin.Users.Commands.CreateUser;

public class CreateUserCommandHandler(
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    ICryptoProvider cryptoProvider,
    IEmailService emailService,
    IConfiguration configuration,
    IMemoryCache memoryCache) : ICommandHandler<CreateUserCommand, IResult<CreateUserCommandResult>>
{
    public async Task<IResult<CreateUserCommandResult>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.GetByEmailAsync(command.Email, cancellationToken);
        if (existingUser != null)
        {
            return ErrorResult<CreateUserCommandResult, string>.Create("Kullanıcı zaten mevcut.");
        }

        var passwordSalt = cryptoProvider.GenerateSalt();
        var passwordHashed = cryptoProvider.Hash(command.Password, passwordSalt);
        
        var user = new User
        {
            Email = command.Email,
            FirstName = command.FirstName,
            MiddleName = command.MiddleName,
            LastName = command.LastName,
            PasswordHashed = passwordHashed,
            PasswordSalt = passwordSalt,
            IsEmailVerified = command.Role == "Admin",
            IsProfileCompleted = command.Role == "Admin"
        };

        if (command.Role == "User")
        {
            user.UserEmailVerification = new UserEmailVerification
            {
                VerificationCode = Guid.NewGuid(),
                ExpiresAt = DateTime.Now.AddDays(7)
            };
        }
        
        var userRole = await roleRepository.GetByNameAsync(command.Role, cancellationToken);
        user.UserRoles.Add(new UserRole
        {
            RoleId = userRole!.Id
        });
        
        await userRepository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        if (command.Role == "User")
        {
            var domain = configuration.GetValue<string>("Domain")!;
            var subject = configuration.GetValue<string>("MailTemplates:EmailVerification:Subject")!;
            var content = configuration.GetValue<string>("MailTemplates:EmailVerification:Content")!
                .Replace("{{FullName}}", user.GetFullName())
                .Replace("{{Domain}}", domain)
                .Replace("{{EmailVerificationCode}}", user.UserEmailVerification.VerificationCode.ToString());
        
            await emailService.SendEmailAsync(user.Email, subject, content);
        
            var uniqueCode = Guid.NewGuid();

            memoryCache.Set(uniqueCode, user.Id, TimeSpan.FromDays(configuration.GetValue<int>("InMemoryCache:ExpirationInHours")));
        
            return SuccessResult<CreateUserCommandResult>.Create(new CreateUserCommandResult(uniqueCode));
        }

        return SuccessResult<CreateUserCommandResult>.Create(new CreateUserCommandResult(Guid.Empty));
    }
}