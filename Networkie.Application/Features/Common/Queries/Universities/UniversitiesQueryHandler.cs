using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Common.Queries.Universities;

public class UniversitiesQueryHandler(IUniversityRepository universityRepository) : IQueryHandler<UniversitiesQuery, IResult<IEnumerable<UniversitiesQueryResult>>>
{
    public async Task<IResult<IEnumerable<UniversitiesQueryResult>>> Handle(UniversitiesQuery query, CancellationToken cancellationToken)
    {
        var universities = await universityRepository.GetAllAsync(cancellationToken);
        return SuccessResult<IEnumerable<UniversitiesQueryResult>>.Create(universities.Select(u => new UniversitiesQueryResult(u.Id, u.Name)));
    }
}