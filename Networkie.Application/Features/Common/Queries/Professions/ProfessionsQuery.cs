using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Common.Queries.Professions;

public record ProfessionsQuery() : IQuery<IResult<IEnumerable<ProfessionsQueryResult>>>;

public record ProfessionsQueryResult(Guid Id, string Name);