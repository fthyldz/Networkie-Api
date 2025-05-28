using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Admin.Professions.Queries.Professions;

public class ProfessionsQueryHandler(
    IProfessionRepository professionRepository)
    : IQueryHandler<ProfessionsQuery, IResult<ProfessionsQueryResult>>
{
    public async Task<IResult<ProfessionsQueryResult>> Handle(ProfessionsQuery query, CancellationToken cancellationToken)
    {
        
        var professions = await professionRepository.GetProfessionsAsPagedForAdmin(query.PageIndex, query.PageSize, query.Search,
            cancellationToken);
        var professionsCount = await professionRepository.GetProfessionsAsPagedTotalCountForAdmin(query.Search, cancellationToken);

        var data = professions.Select(u => new ProfessionsQueryDataResult(
            u.Id,
            u.Name));

        var result = new ProfessionsQueryResult(data, professionsCount);

        return SuccessResult<ProfessionsQueryResult>.Create(result);
    }
}