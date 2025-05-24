using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Common.Queries.Countries;

public class CountriesQueryHandler(ICountryRepository countryRepository) : IQueryHandler<CountriesQuery, IResult<IEnumerable<CountriesQueryResult>>>
{
    public async Task<IResult<IEnumerable<CountriesQueryResult>>> Handle(CountriesQuery query, CancellationToken cancellationToken)
    {
        var countries = await countryRepository.GetAllAsync(cancellationToken);
        return SuccessResult<IEnumerable<CountriesQueryResult>>.Create(countries.Select(c => new CountriesQueryResult(c.Id, c.Name)));
    }
}