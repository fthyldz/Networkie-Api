using FluentValidation;
using Networkie.Entities.Enums;

namespace Networkie.Api.Endpoints.ProfileModule.Models;

public record UpdateProfileDto(string PhoneCountryCode, string PhoneNumber, Gender Gender, DateTime BirthOfDate, bool IsEmployed, bool IsSeekingForJob, bool IsHiring, string? Profession, Guid? ProfessionId, List<UpdateProfileUniversityDto> Universities, string? Country, Guid? CountryId, string? State, Guid? StateId, string? City, Guid? CityId, string? District, Guid? DistrictId, List<UpdateProfileSocialPlatformDto> SocialPlatforms);

public record UpdateProfileSocialPlatformDto(Guid SocialPlatformId, string? Url);

public record UpdateProfileUniversityDto(string? University, Guid? UniversityId, string? Department, Guid? DepartmentId, short? EntryYear);

public class UpdateProfileDtoValidator : AbstractValidator<UpdateProfileDto>
{
    public UpdateProfileDtoValidator()
    {
        RuleFor(x => x.PhoneCountryCode).NotEmpty().NotNull();
        RuleFor(x => x.PhoneNumber).NotEmpty().NotNull();
        RuleFor(x => x.Gender).NotEmpty().NotNull();
        RuleFor(x => x.BirthOfDate).NotEmpty().NotNull();
        RuleFor(x => x.IsEmployed).NotEmpty().NotNull();
        RuleFor(x => x.IsSeekingForJob).NotEmpty().NotNull();
        RuleFor(x => x.IsHiring).NotEmpty().NotNull();
        RuleFor(x => x.SocialPlatforms).NotEmpty().NotNull();
        RuleFor(x => x.Universities).NotEmpty().NotNull();
        
        RuleFor(x => x.Profession).NotEmpty().When(x => !x.ProfessionId.HasValue);
        RuleFor(x => x.ProfessionId).NotEmpty().When(x => string.IsNullOrWhiteSpace(x.Profession));
        
        RuleFor(x => x.Country).NotEmpty().When(x => !x.CountryId.HasValue);
        RuleFor(x => x.CountryId).NotEmpty().When(x => string.IsNullOrWhiteSpace(x.Country));
        
        RuleFor(x => x.State).NotEmpty().When(x => !x.StateId.HasValue);
        RuleFor(x => x.StateId).NotEmpty().When(x => string.IsNullOrWhiteSpace(x.State));
        
        RuleFor(x => x.City).NotEmpty().When(x => !x.CityId.HasValue);
        RuleFor(x => x.CityId).NotEmpty().When(x => string.IsNullOrWhiteSpace(x.City));
        
        RuleFor(x => x.District).NotEmpty().When(x => !x.DistrictId.HasValue);
        RuleFor(x => x.DistrictId).NotEmpty().When(x => string.IsNullOrWhiteSpace(x.District));
    }
}

public class CompleteProfileSocialPlatformDtoValidator : AbstractValidator<UpdateProfileSocialPlatformDto>
{
    public CompleteProfileSocialPlatformDtoValidator()
    {
        RuleFor(x => x.SocialPlatformId).NotEmpty().NotNull();
    }
}

public class CompleteProfileUniversityDtoValidator : AbstractValidator<UpdateProfileUniversityDto>
{
    public CompleteProfileUniversityDtoValidator()
    {
        RuleFor(x => x.University).NotEmpty().When(x => !x.UniversityId.HasValue);
        RuleFor(x => x.UniversityId).NotEmpty().When(x => string.IsNullOrWhiteSpace(x.University));
        
        RuleFor(x => x.Department).NotEmpty().When(x => !x.DepartmentId.HasValue);
        RuleFor(x => x.DepartmentId).NotEmpty().When(x => string.IsNullOrWhiteSpace(x.Department));
    }
}