using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Common.Queries.Professions;

public class ProfessionsQueryHandler(IProfessionRepository professionRepository) : IQueryHandler<ProfessionsQuery, IResult<IEnumerable<ProfessionsQueryResult>>>
{
    public async Task<IResult<IEnumerable<ProfessionsQueryResult>>> Handle(ProfessionsQuery query, CancellationToken cancellationToken)
    {
        var professions = await professionRepository.GetAllAsync(cancellationToken);
        return SuccessResult<IEnumerable<ProfessionsQueryResult>>.Create(professions.Select(u => new ProfessionsQueryResult(u.Id, u.Name)));
    }
}