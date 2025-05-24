using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Providers;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Exceptions;
using Networkie.Application.Common.Results;
using Networkie.Application.Dtos.TokenProviderDtos;
using Networkie.Entities;
using Networkie.Persistence.Abstractions;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Auth.Queries.Login;

public class LoginQueryHandler(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IUserRoleRepository userRoleRepository,
    ICryptoProvider cryptoProvider,
    ITokenProvider tokenProvider,
    IUserTokenRepository userTokenRepository,
    IUnitOfWork unitOfWork)
    : IQueryHandler<LoginQuery, IResult<LoginQueryResult>>
{
    public async Task<IResult<LoginQueryResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(query.Email, cancellationToken);

        if (user == null)
            throw new UnauthorizedException("Invalid email or password.");

        var isEqual = cryptoProvider.HashVerify(query.Password, user.PasswordHashed);

        if (!isEqual)
            throw new UnauthorizedException("Invalid email or password.");

        var tokenResult = tokenProvider.GenerateToken(new GenerateTokenDto(user.Id, query.Email,
            user.UserRoles.Select(ur => ur.Role.Name).ToArray(), user.IsEmailVerified == true,
            user.IsProfileCompleted == true));
        var refreshTokenResult = tokenProvider.GenerateRefreshToken();

        var userToken = await userTokenRepository.GetByUserIdAsync(user.Id, cancellationToken);

        if (userToken != null)
        {
            userTokenRepository.Delete(userToken);
        }

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

        var result = new LoginQueryResult(tokenResult.Token, refreshTokenResult.RefreshToken);

        return SuccessResult<LoginQueryResult>.Create(result);
    }
}