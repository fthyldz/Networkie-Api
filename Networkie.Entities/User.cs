using Networkie.Entities.Abstractions;
using Networkie.Entities.Enums;

namespace Networkie.Entities;

public sealed class User : BaseEntity
{
    public string Email { get; set; }
    public string PasswordHashed { get; set; }
    public string PasswordSalt { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneCountryCode { get; set; }
    public string? PhoneNumber { get; set; }
    public Gender? Gender { get; set; }
    public DateTime? BirthOfDate { get; set; }
    public bool? IsEmployed { get; set; }
    public bool? IsSeekingForJob { get; set; }
    public bool? IsHiring { get; set; }
    public Guid? ProfessionId { get; set; }
    public Profession? Profession { get; set; }
    public Guid? CountryId { get; set; }
    public Country? Country { get; set; }
    public Guid? StateId { get; set; }
    public State? State { get; set; }
    public Guid? CityId { get; set; }
    public City? City { get; set; }
    public Guid? DistrictId { get; set; }
    public District? District { get; set; }
    public bool? IsEmailVerified { get; set; }
    public bool? IsProfileCompleted { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    public UserEmailVerification UserEmailVerification { get; set; }
    public ICollection<UserUniversity> UserUniversities { get; set; } = new HashSet<UserUniversity>();
    public ICollection<UserSocialPlatform> UserSocialPlatforms { get; set; } = new HashSet<UserSocialPlatform>();

}