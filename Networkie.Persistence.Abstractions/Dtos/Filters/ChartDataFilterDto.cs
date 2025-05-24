namespace Networkie.Persistence.Abstractions.Dtos.Filters;

public record ChartDataFilterDto(
    string? FirstName = null,
    string? MiddleName = null,
    string? LastName = null,
    string? Gender = null,
    IEnumerable<Guid>? ProfessionIds = null,
    IEnumerable<Guid>? CountryIds = null,
    IEnumerable<Guid>? StateIds = null,
    IEnumerable<Guid>? CityIds = null,
    IEnumerable<Guid>? DistrictIds = null,
    IEnumerable<Guid>? UniversityIds = null,
    IEnumerable<Guid>? DepartmentIds = null,
    short? EntryYear = null);