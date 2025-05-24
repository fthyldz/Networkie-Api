using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Common.Queries.Universities;

public record UniversitiesQuery() : IQuery<IResult<IEnumerable<UniversitiesQueryResult>>>;

public record UniversitiesQueryResult(Guid Id, string Name);