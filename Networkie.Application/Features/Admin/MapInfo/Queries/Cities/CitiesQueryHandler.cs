using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Admin.MapInfo.Queries.Cities;

public class CitiesQueryHandler(
    ICountryRepository countryRepository,
    ICityRepository cityRepository)
    : IQueryHandler<CitiesQuery, IResult<CitiesQueryResult>>
{
    public async Task<IResult<CitiesQueryResult>> Handle(CitiesQuery query, CancellationToken cancellationToken)
    {
        var countries = string.IsNullOrWhiteSpace(query.SearchCountry) ? await countryRepository.GetAllAsync(cancellationToken) : await countryRepository.GetByContainsNameAsync(query.SearchCountry, cancellationToken);
        
        var cities = await cityRepository.GetCitiesAsPagedForAdmin(query.PageIndex, query.PageSize, query.SearchCity, countries.Select(c => c.Id).ToList(),
            cancellationToken);
        var citiesCount = await cityRepository.GetCitiesAsPagedTotalCountForAdmin(query.SearchCity, countries.Select(c => c.Id).ToList(), cancellationToken);

        var data = cities.Select(c => new CitiesQueryDataResult(
            c.Id,
            c.Name,
            c.CountryId,
            countries.Where(cc => cc.Id == c.CountryId).Select(cc => cc.Name).FirstOrDefault()!));

        var result = new CitiesQueryResult(data, citiesCount);

        return SuccessResult<CitiesQueryResult>.Create(result);
    }
}