using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Entities.Enums;

namespace Networkie.Application.Features.Auth.Commands.CompleteProfile;

public record CompleteProfileCommand(
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
    List<CompleteProfileCommandUniversity> Universities,
    string? Country,
    Guid? CountryId,
    string? State,
    Guid? StateId,
    string? City,
    Guid? CityId,
    string? District,
    Guid? DistrictId,
    List<CompleteProfileCommandSocialPlatform> SocialPlatforms
) : ICommand<IResult<CompleteProfileCommandResult>>;

public record CompleteProfileCommandUniversity(
    string? University,
    Guid? UniversityId,
    string? Department,
    Guid? DepartmentId,
    short? EntryYear
);

public record CompleteProfileCommandSocialPlatform(
    Guid SocialPlatformId,
    string? Url
);

public record CompleteProfileCommandResult();