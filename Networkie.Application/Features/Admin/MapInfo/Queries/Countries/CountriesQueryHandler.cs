using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Admin.MapInfo.Queries.Countries;

public class CountriesQueryHandler(
    ICountryRepository countryRepository)
    : IQueryHandler<CountriesQuery, IResult<CountriesQueryResult>>
{
    public async Task<IResult<CountriesQueryResult>> Handle(CountriesQuery query, CancellationToken cancellationToken)
    {
        
        var countries = await countryRepository.GetCountriesAsPagedForAdmin(query.PageIndex, query.PageSize, query.Search,
            cancellationToken);
        var countriesCount = await countryRepository.GetCountriesAsPagedTotalCountForAdmin(query.Search, cancellationToken);

        var data = countries.Select(u => new CountriesQueryDataResult(
            u.Id,
            u.Name));

        var result = new CountriesQueryResult(data, countriesCount);

        return SuccessResult<CountriesQueryResult>.Create(result);
    }
}