using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Persistence.Abstractions.Dtos.Filters;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Admin.Users.Queries.Users;

public class UsersQueryHandler(
    IUserRepository userRepository)
    : IQueryHandler<UsersQuery, IResult<UsersQueryResult>>
{
    public async Task<IResult<UsersQueryResult>> Handle(UsersQuery query, CancellationToken cancellationToken)
    {
        var filter = new UsersDataFilterDto(query.FirstName, query.MiddleName, query.LastName, query.Email,
            query.PhoneNumber, query.Role);
        var users = await userRepository.GetUsersAsPagedForAdmin(query.PageIndex, query.PageSize, filter,
            cancellationToken);
        var usersCount = await userRepository.GetUsersAsPagedTotalCountForAdmin(filter, cancellationToken);

        var data = users.Select(u => new UsersQueryDataResult(
            u.Id,
            u.FirstName,
            u.Email,
            u.MiddleName,
            u.LastName,
            string.IsNullOrWhiteSpace(u.PhoneCountryCode) ? null : u.PhoneCountryCode + " " + u.PhoneNumber,
            u.UserRoles.OrderBy(r => r.Role.Name).Select(r => r.Role.Name).FirstOrDefault()));

        var result = new UsersQueryResult(data, usersCount);

        return SuccessResult<UsersQueryResult>.Create(result);
    }
}