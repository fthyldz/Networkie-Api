namespace Networkie.Persistence.Abstractions.Dtos.Filters;

public record UsersDataFilterDto(
    string? FirstName = null,
    string? MiddleName = null,
    string? LastName = null,
    string? Email = null,
    string? PhoneNumber = null,
    string? Role = null);