using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Common.Queries.States;

public record StatesQuery(Guid CountryId) : IQuery<IResult<IEnumerable<StatesQueryResult>>>;

public record StatesQueryResult(Guid Id, string Name);