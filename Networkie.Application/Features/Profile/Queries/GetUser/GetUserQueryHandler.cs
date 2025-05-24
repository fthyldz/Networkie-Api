using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Profile.Queries.GetUser;

public class GetUserQueryHandler(IUserRepository userRepository)
    : IQueryHandler<GetUserQuery, IResult<GetUserQueryResult>>
{
    public async Task<IResult<GetUserQueryResult>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdWithDetailAsync(query.UserId, cancellationToken);

        if (user == null)
            return ErrorResult<GetUserQueryResult, string>.Create("User not found");

        return SuccessResult<GetUserQueryResult>.Create(new GetUserQueryResult(
            user.Id,
            user.Email,
            user.FirstName,
            user.MiddleName,
            user.LastName,
            user.PhoneCountryCode!,
            user.PhoneNumber!,
            user.Gender!.Value,
            user.BirthOfDate!.Value,
            user.IsEmployed!.Value,
            user.IsSeekingForJob!.Value,
            user.IsHiring!.Value,
            user.ProfessionId!.Value,
            user.UserUniversities.Select(uu =>
                new GetUserQueryResultUniversity(uu.UniversityId, uu.DepartmentId, uu.EntryYear)),
            user.CountryId!.Value,
            user.StateId!.Value,
            user.CityId!.Value,
            user.DistrictId!.Value,
            user.UserSocialPlatforms.Select(usp => new GetUserQueryResultSocialPlatform(usp.SocialPlatformId, usp.Url))
        ));
    }
}