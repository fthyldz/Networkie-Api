using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Exceptions;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Auth.Queries.ProfileCompleted;

public class ProfileCompletedQueryHandler(IUserRepository userRepository) : IQueryHandler<ProfileCompletedQuery, IResult<ProfileCompletedQueryResult>>
{
    public async Task<IResult<ProfileCompletedQueryResult>> Handle(ProfileCompletedQuery query, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(query.UserId, cancellationToken);
        if (user == null)
            throw new UnauthorizedException("User Not Found");
        
        var result = new ProfileCompletedQueryResult(user.IsProfileCompleted.HasValue && user.IsProfileCompleted.Value);
        return SuccessResult<ProfileCompletedQueryResult>.Create(result);
    }
}