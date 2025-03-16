using Networkie.Entities.Abstractions;

namespace Networkie.Entities;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string PasswordHashed { get; set; }
    public string PasswordSalt { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string PhoneCountryCode { get; set; }
    public string PhoneNumber { get; set; }
    public byte Gender { get; set; }
    public DateTime BirthOfDate { get; set; }
    public bool IsEmployed { get; set; }
    public bool IsHiring { get; set; }
    public Guid ProfessionId { get; set; }
    public Guid CountryId { get; set; }
    public Guid? StateId { get; set; }
    public Guid CityId { get; set; }
    public Guid DistrictId { get; set; }
}