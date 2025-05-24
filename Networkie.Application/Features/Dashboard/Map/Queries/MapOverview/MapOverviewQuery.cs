using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Dashboard.Map.Queries.MapOverview;

public record MapOverviewQuery(
    string? FirstName = null,
    string? MiddleName = null,
    string? LastName = null,
    string? Gender = null,
    string? Profession = null,
    string? Country = null,
    string? State = null,
    string? City = null,
    string? District = null,
    string? University = null,
    string? Department = null,
    short? EntryYear = null) : IQuery<IResult<IEnumerable<MapOverviewQueryResult>>>;
    
public record MapOverviewQueryResult(string Keyword);