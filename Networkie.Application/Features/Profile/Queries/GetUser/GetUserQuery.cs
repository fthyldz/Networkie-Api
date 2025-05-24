using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Entities.Enums;

namespace Networkie.Application.Features.Profile.Queries.GetUser;

public record GetUserQuery(Guid UserId) : IQuery<IResult<GetUserQueryResult>>;

public record GetUserQueryResult(
    Guid UserId,
    string Email,
    string FirstName,
    string? MiddleName,
    string? LastName,
    string PhoneCountryCode,
    string PhoneNumber,
    Gender Gender,
    DateTime BirthOfDate,
    bool IsEmployed,
    bool IsSeekingForJob,
    bool IsHiring,
    Guid ProfessionId,
    IEnumerable<GetUserQueryResultUniversity> Universities,
    Guid CountryId,
    Guid StateId,
    Guid CityId,
    Guid DistrictId,
    IEnumerable<GetUserQueryResultSocialPlatform> SocialPlatforms);
    
public record GetUserQueryResultUniversity(
    Guid UniversityId,
    Guid DepartmentId,
    short? EntryYear);
public record GetUserQueryResultSocialPlatform(
    Guid SocialPlatformId,
    string Url);
