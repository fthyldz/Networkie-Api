using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Common.Queries.States;

public class StatesQueryHandler(IStateRepository stateRepository) : IQueryHandler<StatesQuery, IResult<IEnumerable<StatesQueryResult>>>
{
    public async Task<IResult<IEnumerable<StatesQueryResult>>> Handle(StatesQuery query, CancellationToken cancellationToken)
    {
        var states = await stateRepository.GetAllByCountryIdAsync(query.CountryId, cancellationToken);
        return SuccessResult<IEnumerable<StatesQueryResult>>.Create(states.Select(c => new StatesQueryResult(c.Id, c.Name)));
    }
}