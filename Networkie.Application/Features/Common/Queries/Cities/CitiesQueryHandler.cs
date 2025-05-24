using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Common.Queries.Cities;

public class CitiesQueryHandler(ICityRepository cityRepository) : IQueryHandler<CitiesQuery, IResult<IEnumerable<CitiesQueryResult>>>
{
    public async Task<IResult<IEnumerable<CitiesQueryResult>>> Handle(CitiesQuery query,
        CancellationToken cancellationToken)
    {
        var cities =
            await cityRepository.GetAllByCountryIdOrStateIdAsync(query.CountryId, query.StateId, cancellationToken);
        return SuccessResult<IEnumerable<CitiesQueryResult>>.Create(cities.Select(c =>
            new CitiesQueryResult(c.Id, c.Name)));
    }
}