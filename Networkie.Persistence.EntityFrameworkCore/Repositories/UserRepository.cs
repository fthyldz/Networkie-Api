using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Networkie.Entities;
using Networkie.Entities.Enums;
using Networkie.Persistence.Abstractions.Dtos.Filters;
using Networkie.Persistence.Abstractions.Dtos.Result;
using Networkie.Persistence.Abstractions.Repositories;
using Networkie.Persistence.EntityFrameworkCore.Contexts;

namespace Networkie.Persistence.EntityFrameworkCore.Repositories;

public class UserRepository(IEfCoreDbContext context) : Repository<User>(context), IUserRepository
{
    public async Task<User?> GetByIdWithRolesAsync(Guid id, CancellationToken cancellationToken = default)
        => await TableAsNoTracking.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        => await TableAsNoTracking.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    private Expression<Func<User, bool>> GenerateChartDataFilter(ChartDataFilterDto? filter = null)
    {
        Expression<Func<User, bool>> filterFunc = u =>
            filter == null || (string.IsNullOrWhiteSpace(filter.FirstName) ||
                               EF.Functions.ILike(u.FirstName, $"%{filter.FirstName}%"))
            && (string.IsNullOrWhiteSpace(filter.MiddleName) || !string.IsNullOrWhiteSpace(u.MiddleName) &&
                EF.Functions.ILike(u.MiddleName, $"%{filter.MiddleName}%"))
            && (string.IsNullOrWhiteSpace(filter.LastName) || !string.IsNullOrWhiteSpace(u.LastName) &&
                EF.Functions.ILike(u.LastName, $"%{filter.LastName}%"))
            && (filter.ProfessionIds == null ||
                u.ProfessionId.HasValue && filter.ProfessionIds.Contains(u.ProfessionId.Value))
            && (filter.CountryIds == null || u.CountryId.HasValue && filter.CountryIds.Contains(u.CountryId.Value))
            && (filter.StateIds == null || u.StateId.HasValue && filter.StateIds.Contains(u.StateId.Value))
            && (filter.CityIds == null || u.CityId.HasValue && filter.CityIds.Contains(u.CityId.Value))
            && (filter.DistrictIds == null ||
                u.DistrictId.HasValue && filter.DistrictIds.Contains(u.DistrictId.Value))
            && (filter.UniversityIds == null ||
                u.UserUniversities.Any(uu => filter.UniversityIds.Contains(uu.UniversityId)))
            && (filter.DepartmentIds == null ||
                u.UserUniversities.Any(uu => filter.DepartmentIds.Contains(uu.DepartmentId)))
            && (filter.EntryYear == null || u.UserUniversities.Any(uu => uu.EntryYear == filter.EntryYear));
        return filterFunc;
    }
    
    private Expression<Func<User, bool>> GenerateMapDataFilter(MapDataFilterDto? filter = null)
    {
        Expression<Func<User, bool>> filterFunc = u =>
            filter == null || (string.IsNullOrWhiteSpace(filter.FirstName) ||
                               EF.Functions.ILike(u.FirstName, $"%{filter.FirstName}%"))
            && (string.IsNullOrWhiteSpace(filter.MiddleName) || !string.IsNullOrWhiteSpace(u.MiddleName) &&
                EF.Functions.ILike(u.MiddleName, $"%{filter.MiddleName}%"))
            && (string.IsNullOrWhiteSpace(filter.LastName) || !string.IsNullOrWhiteSpace(u.LastName) &&
                EF.Functions.ILike(u.LastName, $"%{filter.LastName}%"))
            && (filter.ProfessionIds == null ||
                u.ProfessionId.HasValue && filter.ProfessionIds.Contains(u.ProfessionId.Value))
            && (filter.CountryIds == null || u.CountryId.HasValue && filter.CountryIds.Contains(u.CountryId.Value))
            && (filter.StateIds == null || u.StateId.HasValue && filter.StateIds.Contains(u.StateId.Value))
            && (filter.CityIds == null || u.CityId.HasValue && filter.CityIds.Contains(u.CityId.Value))
            && (filter.DistrictIds == null ||
                u.DistrictId.HasValue && filter.DistrictIds.Contains(u.DistrictId.Value))
            && (filter.UniversityIds == null ||
                u.UserUniversities.Any(uu => filter.UniversityIds.Contains(uu.UniversityId)))
            && (filter.DepartmentIds == null ||
                u.UserUniversities.Any(uu => filter.DepartmentIds.Contains(uu.DepartmentId)))
            && (filter.EntryYear == null || u.UserUniversities.Any(uu => uu.EntryYear == filter.EntryYear));
        return filterFunc;
    }

    public async Task<IEnumerable<User>> GetUsersAsPaged(int pageIndex = 0, int pageSize = 25,
        ListDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
        => await TableAsNoTracking
            .Include(u => u.Profession)
            .Include(u => u.Country)
            .Include(u => u.State)
            .Include(u => u.City)
            .Include(u => u.District)
            .Include(u => u.UserUniversities)
            .ThenInclude(uu => uu.University)
            .Include(u => u.UserUniversities)
            .ThenInclude(uu => uu.Department)
            .Where(u =>
                filter == null || (string.IsNullOrWhiteSpace(filter.FirstName) ||
                                   EF.Functions.ILike(u.FirstName, $"%{filter.FirstName}%"))
                && (string.IsNullOrWhiteSpace(filter.MiddleName) || !string.IsNullOrWhiteSpace(u.MiddleName) &&
                    EF.Functions.ILike(u.MiddleName, $"%{filter.MiddleName}%"))
                && (string.IsNullOrWhiteSpace(filter.LastName) || !string.IsNullOrWhiteSpace(u.LastName) &&
                    EF.Functions.ILike(u.LastName, $"%{filter.LastName}%"))
                && (filter.ProfessionIds == null ||
                    u.ProfessionId.HasValue && filter.ProfessionIds.Contains(u.ProfessionId.Value))
                && (filter.CountryIds == null || u.CountryId.HasValue && filter.CountryIds.Contains(u.CountryId.Value))
                && (filter.StateIds == null || u.StateId.HasValue && filter.StateIds.Contains(u.StateId.Value))
                && (filter.CityIds == null || u.CityId.HasValue && filter.CityIds.Contains(u.CityId.Value))
                && (filter.DistrictIds == null ||
                    u.DistrictId.HasValue && filter.DistrictIds.Contains(u.DistrictId.Value))
                && (filter.UniversityIds == null ||
                    u.UserUniversities.Any(uu => filter.UniversityIds.Contains(uu.UniversityId)))
                && (filter.DepartmentIds == null ||
                    u.UserUniversities.Any(uu => filter.DepartmentIds.Contains(uu.DepartmentId)))
                && (filter.EntryYear == null || u.UserUniversities.Any(uu => uu.EntryYear == filter.EntryYear))
            ).SelectMany(u => u.UserUniversities
                .Where(uu => filter == null || filter.EntryYear == null || uu.EntryYear == filter.EntryYear)
                .Select(uu => new User()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    MiddleName = u.MiddleName,
                    LastName = u.LastName,
                    Gender = u.Gender,
                    ProfessionId = u.ProfessionId,
                    Profession = u.Profession,
                    CountryId = u.CountryId,
                    Country = u.Country,
                    StateId = u.StateId,
                    State = u.State,
                    CityId = u.CityId,
                    City = u.City,
                    DistrictId = u.DistrictId,
                    District = u.District,
                    UserUniversities = new List<UserUniversity>
                    {
                        new()
                        {
                            UniversityId = uu.UniversityId,
                            University = uu.University,
                            DepartmentId = uu.DepartmentId,
                            Department = uu.Department,
                            EntryYear = uu.EntryYear
                        }
                    }
                })).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken);

    public async Task<long> GetUsersAsPagedTotalCount(ListDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
        => await TableAsNoTracking
            .Include(u => u.UserUniversities)
            .Where(u =>
                filter == null || (string.IsNullOrWhiteSpace(filter.FirstName) ||
                                   EF.Functions.ILike(u.FirstName, $"%{filter.FirstName}%"))
                && (string.IsNullOrWhiteSpace(filter.MiddleName) || !string.IsNullOrWhiteSpace(u.MiddleName) &&
                    EF.Functions.ILike(u.MiddleName, $"%{filter.MiddleName}%"))
                && (string.IsNullOrWhiteSpace(filter.LastName) || !string.IsNullOrWhiteSpace(u.LastName) &&
                    EF.Functions.ILike(u.LastName, $"%{filter.LastName}%"))
                && (filter.ProfessionIds == null ||
                    u.ProfessionId.HasValue && filter.ProfessionIds.Contains(u.ProfessionId.Value))
                && (filter.CountryIds == null || u.CountryId.HasValue && filter.CountryIds.Contains(u.CountryId.Value))
                && (filter.StateIds == null || u.StateId.HasValue && filter.StateIds.Contains(u.StateId.Value))
                && (filter.CityIds == null || u.CityId.HasValue && filter.CityIds.Contains(u.CityId.Value))
                && (filter.DistrictIds == null ||
                    u.DistrictId.HasValue && filter.DistrictIds.Contains(u.DistrictId.Value))
                && (filter.UniversityIds == null ||
                    u.UserUniversities.Any(uu => filter.UniversityIds.Contains(uu.UniversityId)))
                && (filter.DepartmentIds == null ||
                    u.UserUniversities.Any(uu => filter.DepartmentIds.Contains(uu.DepartmentId)))
                && (filter.EntryYear == null || u.UserUniversities.Any(uu => uu.EntryYear == filter.EntryYear))
            ).SelectMany(u =>
                u.UserUniversities
                    .Where(uu => filter == null || filter.EntryYear == null || uu.EntryYear == filter.EntryYear)
                    .Select(uu => u)).CountAsync(cancellationToken);

    public async Task<GenderPieChartDataDto> GetGenderPieChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        var filterFunc = GenerateChartDataFilter(filter);

        var query = TableAsNoTracking.Where(filterFunc);

        var maleCount = await query.Where(u => u.Gender.HasValue && u.Gender == Gender.Male)
            .CountAsync(cancellationToken);
        var femaleCount = await query.Where(u => u.Gender.HasValue && u.Gender == Gender.Female)
            .CountAsync(cancellationToken);

        return new GenderPieChartDataDto(maleCount, femaleCount);
    }

    public async Task<IEnumerable<AgeBarChartDataDto>> GetAgeBarChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        var filterFunc = GenerateChartDataFilter(filter);

        var query = TableAsNoTracking.Where(filterFunc);

        return await query
            .Where(u => u.BirthOfDate.HasValue)
            .Select(u =>
                DateTime.Now.Year - u.BirthOfDate!.Value.Year -
                (DateTime.Now.DayOfYear < u.BirthOfDate.Value.DayOfYear ? 1 : 0))
            .GroupBy(a => a)
            .Select(a => new AgeBarChartDataDto(a.Key, a.LongCount()))
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<UniversityChartDataDto>> GetUniversityChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        var filterFunc = GenerateChartDataFilter(filter);

        var query = TableAsNoTracking.Where(filterFunc);

        return await query
            .Include(u => u.UserUniversities)
            .ThenInclude(uu => uu.University)
            .SelectMany(u => u.UserUniversities)
            .Where(uu => filter == null || !filter.EntryYear.HasValue || filter.EntryYear.HasValue && uu.EntryYear == filter.EntryYear)
            .GroupBy(uu => new { uu.UniversityId, uu.University.Name })
            .Select(uu => new UniversityChartDataDto(uu.Key.Name, uu.LongCount()))
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<DepartmentChartDataDto>> GetDepartmentChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        var filterFunc = GenerateChartDataFilter(filter);

        var query = TableAsNoTracking.Where(filterFunc);

        return await query
            .Include(u => u.UserUniversities)
            .ThenInclude(uu => uu.University)
            .SelectMany(u => u.UserUniversities)
            .Where(uu => filter == null || !filter.EntryYear.HasValue || filter.EntryYear.HasValue && uu.EntryYear == filter.EntryYear)
            .GroupBy(uu => new { uu.DepartmentId, uu.Department.Name })
            .Select(uu => new DepartmentChartDataDto(uu.Key.Name, uu.LongCount()))
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<ProfessionChartDataDto>> GetProfessionChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        var filterFunc = GenerateChartDataFilter(filter);

        var query = TableAsNoTracking.Where(filterFunc);

        return await query
            .Include(u => u.Profession)
            .Where(u => u.Profession != null)
            .Select(u => u.Profession)
            .GroupBy(p => p.Id)
            .Select(p => new ProfessionChartDataDto(p.Select(pp => pp.Name).FirstOrDefault(), p.LongCount()))
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<CountryChartDataDto>> GetCountryChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        var filterFunc = GenerateChartDataFilter(filter);

        var query = TableAsNoTracking.Where(filterFunc).Include(u => u.Country);
        
        return await query
            .Where(u => u.Country != null)
            .Select(u => u.Country)
            .GroupBy(c => c!.Id)
            .Select(c => new CountryChartDataDto(c.Select(cc => cc!.Name).FirstOrDefault()!, c.LongCount()))
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<CityChartDataDto>> GetCityChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        var filterFunc = GenerateChartDataFilter(filter);

        var query = TableAsNoTracking.Where(filterFunc).Include(u => u.Country);
        
        return await query
            .Where(u => u.City != null)
            .Select(u => u.City)
            .GroupBy(c => c!.Id)
            .Select(c => new CityChartDataDto(c.Select(cc => cc!.Name).FirstOrDefault()!, c.LongCount()))
            .ToListAsync(cancellationToken);
    }
    
    public async Task<EmploymentChartDataDto> GetEmploymentChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        var filterFunc = GenerateChartDataFilter(filter);

        var query = TableAsNoTracking.Where(filterFunc);

        var isEmployed = await query.Where(u => u.IsEmployed.HasValue && u.IsEmployed.Value)
            .CountAsync(cancellationToken);
        var isNotEmployed = await query.Where(u => u.IsEmployed.HasValue && !u.IsEmployed.Value)
            .CountAsync(cancellationToken);

        return new EmploymentChartDataDto(isEmployed, isNotEmployed);
    }
    
    public async Task<EmploymentStatusChartDataDto> GetEmploymentStatusChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        var filterFunc = GenerateChartDataFilter(filter);

        var query = TableAsNoTracking.Where(filterFunc);

        var isSeekingForJob = await query.Where(u => u.IsSeekingForJob.HasValue && u.IsSeekingForJob.Value)
            .CountAsync(cancellationToken);
        var isHiring = await query.Where(u => u.IsHiring.HasValue && u.IsHiring.Value)
            .CountAsync(cancellationToken);

        return new EmploymentStatusChartDataDto(isSeekingForJob, isHiring);
    }
    
    public async Task<IEnumerable<SocialPlatformChartDataDto>> GetSocialPlatformChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        var filterFunc = GenerateChartDataFilter(filter);

        var query = TableAsNoTracking.Where(filterFunc);

        return await query
            .Include(u => u.UserSocialPlatforms)
            .ThenInclude(usp => usp.SocialPlatform)
            .SelectMany(u => u.UserSocialPlatforms)
            .GroupBy(usp => new { usp.SocialPlatformId, usp.SocialPlatform.Name })
            .Select(uu => new SocialPlatformChartDataDto(uu.Key.Name, uu.LongCount()))
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<MapOverviewDataDto>> GetMapOverviewData(MapDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        var filterFunc = GenerateMapDataFilter(filter);

        var query = TableAsNoTracking.Where(filterFunc);

        return await query
            .Where(u => u.CountryId.HasValue && u.StateId.HasValue && u.CityId.HasValue)
            .Include(u => u.Country)
            .Include(u => u.State)
            .Include(u => u.City)
            .Select(u => new MapOverviewDataDto($"{u.Country!.Name}, {u.State!.Name}, {u.City!.Name}"))
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task<User?> GetByIdWithDetailAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await TableAsNoTracking
            .Include(u => u.UserUniversities)
            .Include(u => u.UserSocialPlatforms)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }
    
    public async Task<User?> GetByIdWithDetailToUpdateAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await Table
            .Include(u => u.UserUniversities)
            .Include(u => u.UserSocialPlatforms)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }
    
    public async Task<IEnumerable<User>> GetUsersAsPagedForAdmin(int pageIndex = 0, int pageSize = 25,
        UsersDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
        => await TableAsNoTracking
            .Include(u => u.Profession)
            .Include(u => u.Country)
            .Include(u => u.State)
            .Include(u => u.City)
            .Include(u => u.District)
            .Include(u => u.UserUniversities)
            .ThenInclude(uu => uu.University)
            .Include(u => u.UserUniversities)
            .ThenInclude(uu => uu.Department)
            .Include(ur => ur.UserRoles)
            .ThenInclude(r => r.Role)
            .Where(u =>
                filter == null || (string.IsNullOrWhiteSpace(filter.FirstName) ||
                                   EF.Functions.ILike(u.FirstName, $"%{filter.FirstName}%"))
                && (string.IsNullOrWhiteSpace(filter.MiddleName) || !string.IsNullOrWhiteSpace(u.MiddleName) &&
                    EF.Functions.ILike(u.MiddleName, $"%{filter.MiddleName}%"))
                && (string.IsNullOrWhiteSpace(filter.LastName) || !string.IsNullOrWhiteSpace(u.LastName) &&
                    EF.Functions.ILike(u.LastName, $"%{filter.LastName}%"))
                && (string.IsNullOrWhiteSpace(filter.Email) || !string.IsNullOrWhiteSpace(u.Email) &&
                    EF.Functions.ILike(u.Email, $"%{filter.Email}%"))
                && (string.IsNullOrWhiteSpace(filter.PhoneNumber) || !string.IsNullOrWhiteSpace(u.PhoneCountryCode) &&
                    EF.Functions.ILike(u.PhoneCountryCode, $"%{filter.PhoneNumber}%"))
                && (string.IsNullOrWhiteSpace(filter.PhoneNumber) || !string.IsNullOrWhiteSpace(u.PhoneNumber) &&
                    EF.Functions.ILike(u.PhoneNumber, $"%{filter.PhoneNumber}%"))
                && (string.IsNullOrWhiteSpace(filter.Role) || 
                    EF.Functions.ILike(u.UserRoles.OrderBy(r => r.Role.Name).Select(r => r.Role.Name).FirstOrDefault()!, $"%{filter.Role}%"))
                ).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken);

    public async Task<long> GetUsersAsPagedTotalCountForAdmin(UsersDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
        => await TableAsNoTracking
            .Include(u => u.UserUniversities)
            .Include(ur => ur.UserRoles)
            .ThenInclude(r => r.Role)
            .Where(u =>
                filter == null || (string.IsNullOrWhiteSpace(filter.FirstName) ||
                                   EF.Functions.ILike(u.FirstName, $"%{filter.FirstName}%"))
                && (string.IsNullOrWhiteSpace(filter.MiddleName) || !string.IsNullOrWhiteSpace(u.MiddleName) &&
                    EF.Functions.ILike(u.MiddleName, $"%{filter.MiddleName}%"))
                && (string.IsNullOrWhiteSpace(filter.LastName) || !string.IsNullOrWhiteSpace(u.LastName) &&
                    EF.Functions.ILike(u.LastName, $"%{filter.LastName}%"))
                && (string.IsNullOrWhiteSpace(filter.Email) || !string.IsNullOrWhiteSpace(u.Email) &&
                    EF.Functions.ILike(u.Email, $"%{filter.Email}%"))
                && (string.IsNullOrWhiteSpace(filter.PhoneNumber) || !string.IsNullOrWhiteSpace(u.PhoneCountryCode) &&
                    EF.Functions.ILike(u.PhoneCountryCode, $"%{filter.PhoneNumber}%"))
                && (string.IsNullOrWhiteSpace(filter.PhoneNumber) || !string.IsNullOrWhiteSpace(u.PhoneNumber) &&
                    EF.Functions.ILike(u.PhoneNumber, $"%{filter.PhoneNumber}%"))
                && (string.IsNullOrWhiteSpace(filter.Role) || 
                    EF.Functions.ILike(u.UserRoles.OrderBy(r => r.Role.Name).Select(r => r.Role.Name).FirstOrDefault()!, $"%{filter.Role}%"))
            ).CountAsync(cancellationToken);
}