using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Admin.Universities.Queries.Universities;

public class UniversitiesQueryHandler(
    IUniversityRepository universityRepository)
    : IQueryHandler<UniversitiesQuery, IResult<UniversitiesQueryResult>>
{
    public async Task<IResult<UniversitiesQueryResult>> Handle(UniversitiesQuery query, CancellationToken cancellationToken)
    {
        
        var universities = await universityRepository.GetUniversitiesAsPagedForAdmin(query.PageIndex, query.PageSize, query.Search,
            cancellationToken);
        var universitiesCount = await universityRepository.GetUniversitiesAsPagedTotalCountForAdmin(query.Search, cancellationToken);

        var data = universities.Select(u => new UniversitiesQueryDataResult(
            u.Id,
            u.Name));

        var result = new UniversitiesQueryResult(data, universitiesCount);

        return SuccessResult<UniversitiesQueryResult>.Create(result);
    }
}