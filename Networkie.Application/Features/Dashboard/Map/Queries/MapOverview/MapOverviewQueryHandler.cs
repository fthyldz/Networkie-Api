using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions.Dtos.Filters;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Dashboard.Map.Queries.MapOverview;

public class MapOverviewQueryHandler(
    IUserRepository userRepository,
    IProfessionRepository professionRepository,
    ICountryRepository countryRepository,
    IStateRepository stateRepository,
    ICityRepository cityRepository,
    IDistrictRepository districtRepository,
    IUniversityRepository universityRepository,
    IDepartmentRepository departmentRepository)
    : IQueryHandler<MapOverviewQuery, IResult<IEnumerable<MapOverviewQueryResult>>>
{
    public async Task<IResult<IEnumerable<MapOverviewQueryResult>>> Handle(MapOverviewQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<Guid>? professionIds = null;
        if (!string.IsNullOrWhiteSpace(query.Profession))
        {
            var professions = await professionRepository.GetByContainsNameAsync(query.Profession, cancellationToken);
            professionIds = professions.Select(p => p.Id);
        }
        
        IEnumerable<Guid>? countryIds = null;
        if (!string.IsNullOrWhiteSpace(query.Country))
        {
            var countries = await countryRepository.GetByContainsNameAsync(query.Country, cancellationToken);
            countryIds = countries.Select(c => c.Id);
        }
        
        IEnumerable<Guid>? stateIds = null;
        if (!string.IsNullOrWhiteSpace(query.State))
        {
            var states = await stateRepository.GetByContainsNameAsync(query.State, cancellationToken);
            stateIds = states.Select(s => s.Id);
        }
        
        IEnumerable<Guid>? cityIds = null;
        if (!string.IsNullOrWhiteSpace(query.City))
        {
            var cities = await cityRepository.GetByContainsNameAsync(query.City, cancellationToken);
            cityIds = cities.Select(c => c.Id);
        }
        
        IEnumerable<Guid>? districtIds = null;
        if (!string.IsNullOrWhiteSpace(query.District))
        {
            var districts = await districtRepository.GetByContainsNameAsync(query.District, cancellationToken);
            districtIds = districts.Select(d => d.Id);
        }
        
        IEnumerable<Guid>? universityIds = null;
        if (!string.IsNullOrWhiteSpace(query.University))
        {
            var universities = await universityRepository.GetByContainsNameAsync(query.University, cancellationToken);
            universityIds = universities.Select(u => u.Id);
        }
        
        IEnumerable<Guid>? departmentIds = null;
        if (!string.IsNullOrWhiteSpace(query.Department))
        {
            var departments = await departmentRepository.GetByContainsNameAsync(query.Department, cancellationToken);
            departmentIds = departments.Select(d => d.Id);
        }
        
        var filter = new MapDataFilterDto(query.FirstName, query.MiddleName, query.LastName, query.Gender, professionIds, countryIds, stateIds, cityIds, districtIds, universityIds, departmentIds, query.EntryYear);
        var mapOverviewData = await userRepository.GetMapOverviewData(filter, cancellationToken);
        
        var result = mapOverviewData.Select(x => new MapOverviewQueryResult(x.Keyword));
        
        return SuccessResult<IEnumerable<MapOverviewQueryResult>>.Create(result);
    }
}