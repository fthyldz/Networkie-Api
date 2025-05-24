using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Entities.Enums;

namespace Networkie.Application.Features.Profile.Commands.UpdateProfile;

public record UpdateProfileCommand(
    Guid UserId,
    string PhoneCountryCode,
    string PhoneNumber,
    Gender Gender,
    DateTime BirthOfDate,
    bool IsEmployed,
    bool IsSeekingForJob,
    bool IsHiring,
    string? Profession,
    Guid? ProfessionId,
    List<UpdateProfileCommandUniversity> Universities,
    string? Country,
    Guid? CountryId,
    string? State,
    Guid? StateId,
    string? City,
    Guid? CityId,
    string? District,
    Guid? DistrictId,
    List<UpdateProfileCommandSocialPlatform> SocialPlatforms
) : ICommand<IResult<UpdateProfileCommandResult>>;

public record UpdateProfileCommandUniversity(
    string? University,
    Guid? UniversityId,
    string? Department,
    Guid? DepartmentId,
    short? EntryYear
);

public record UpdateProfileCommandSocialPlatform(
    Guid SocialPlatformId,
    string? Url
);

public record UpdateProfileCommandResult();