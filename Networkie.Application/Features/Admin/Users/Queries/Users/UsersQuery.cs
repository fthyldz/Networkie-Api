using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Admin.Users.Queries.Users;

public record UsersQuery(int PageIndex = 0, int PageSize = 25,
    string? FirstName = null,
    string? MiddleName = null,
    string? LastName = null,
    string? Email = null,
    string? PhoneNumber = null,
    string? Role = null) : IQuery<IResult<UsersQueryResult>>;

public record UsersQueryResult(IEnumerable<UsersQueryDataResult> Data, long TotalCount);

public record UsersQueryDataResult(
    Guid Id,
    string FirstName,
    string Email,
    string? MiddleName = null,
    string? LastName = null,
    string? PhoneNumber = null,
    string? Role = null);