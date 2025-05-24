using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Providers;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Exceptions;
using Networkie.Application.Common.Results;
using Networkie.Application.Dtos.TokenProviderDtos;
using Networkie.Entities;
using Networkie.Persistence.Abstractions;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler(
    IUserRepository userRepository,
    ITokenProvider tokenProvider,
    IUserTokenRepository userTokenRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RefreshTokenCommand, IResult<RefreshTokenCommandResult>>
{
    public async Task<IResult<RefreshTokenCommandResult>> Handle(RefreshTokenCommand command,
        CancellationToken cancellationToken)
    {
        var userToken = await userTokenRepository.GetByRefreshTokenAsync(command.RefreshToken, cancellationToken);

        if (userToken == null)
            throw new UnauthorizedException("Invalid email or password.");

        var user = await userRepository.GetByIdAsync(userToken.UserId, cancellationToken);

        if (user == null)
            throw new UnauthorizedException("Invalid email or password.");

        var tokenResult = tokenProvider.GenerateToken(new GenerateTokenDto(user.Id, user.Email,
            user.UserRoles.Select(ur => ur.Role.Name).ToArray(), user.IsEmailVerified == true,
            user.IsProfileCompleted == true));
        var refreshTokenResult = tokenProvider.GenerateRefreshToken();

        userTokenRepository.Delete(userToken);

        var newUserToken = new UserToken
        {
            UserId = user.Id,
            Token = tokenResult.Token,
            TokenCreatedAt = tokenResult.CreatedAt,
            TokenExpiresAt = tokenResult.ExpiresAt,
            RefreshToken = refreshTokenResult.RefreshToken,
            RefreshTokenCreatedAt = refreshTokenResult.CreatedAt,
            RefreshTokenExpiresAt = refreshTokenResult.ExpiresAt
        };

        await userTokenRepository.AddAsync(newUserToken, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var result = new RefreshTokenCommandResult(tokenResult.Token, refreshTokenResult.RefreshToken);

        return SuccessResult<RefreshTokenCommandResult>.Create(result);
    }
}