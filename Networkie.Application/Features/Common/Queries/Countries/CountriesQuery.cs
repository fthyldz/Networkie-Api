using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Common.Queries.Countries;

public record CountriesQuery() : IQuery<IResult<IEnumerable<CountriesQueryResult>>>;

public record CountriesQueryResult(Guid Id, string Name);