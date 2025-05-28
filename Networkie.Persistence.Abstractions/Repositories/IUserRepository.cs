using Networkie.Entities;
using Networkie.Persistence.Abstractions.Dtos.Filters;
using Networkie.Persistence.Abstractions.Dtos.Result;

namespace Networkie.Persistence.Abstractions.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByIdWithRolesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<IEnumerable<User>> GetUsersAsPaged(int pageIndex = 0, int pageSize = 25, ListDataFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    Task<long> GetUsersAsPagedTotalCount(ListDataFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    Task<GenderPieChartDataDto> GetGenderPieChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<AgeBarChartDataDto>> GetAgeBarChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<UniversityChartDataDto>> GetUniversityChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<DepartmentChartDataDto>> GetDepartmentChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<ProfessionChartDataDto>> GetProfessionChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<CountryChartDataDto>> GetCountryChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<CityChartDataDto>> GetCityChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    Task<EmploymentChartDataDto> GetEmploymentChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    Task<EmploymentStatusChartDataDto> GetEmploymentStatusChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<SocialPlatformChartDataDto>> GetSocialPlatformChart(ChartDataFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<MapOverviewDataDto>> GetMapOverviewData(MapDataFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    Task<User?> GetByIdWithDetailAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<User?> GetByIdWithDetailToUpdateAsync(Guid userId, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<User>> GetUsersAsPagedForAdmin(int pageIndex = 0, int pageSize = 25, UsersDataFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    Task<long> GetUsersAsPagedTotalCountForAdmin(UsersDataFilterDto? filter = null,
        CancellationToken cancellationToken = default);
}