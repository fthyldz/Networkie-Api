using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Entities.Enums;

namespace Networkie.Application.Features.Dashboard.Queries.ListData;

public record ListDataQuery(int PageIndex = 0, int PageSize = 25,
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
    short? EntryYear = null) : IQuery<IResult<ListDataQueryResult>>;

public record ListDataQueryResult(IEnumerable<ListDataQueryDataResult> Data, long TotalCount);

public record ListDataQueryDataResult(
    string FirstName,
    string? MiddleName,
    string? LastName,
    string? Gender,
    string? Profession,
    string? Country,
    string? State,
    string? City,
    string? District,
    string? University,
    string? Department,
    string? EntryYear);