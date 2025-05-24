using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Common.Queries.Districts;

public class DistrictsQueryHandler(IDistrictRepository districtRepository) : IQueryHandler<DistrictsQuery, IResult<IEnumerable<DistrictsQueryResult>>>
{
    public async Task<IResult<IEnumerable<DistrictsQueryResult>>> Handle(DistrictsQuery query, CancellationToken cancellationToken)
    {
        var districts = await districtRepository.GetAllByCityIdAsync(query.CityId, cancellationToken);
        return SuccessResult<IEnumerable<DistrictsQueryResult>>.Create(districts.Select(c => new DistrictsQueryResult(c.Id, c.Name)));
    }
}