using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Auth.Queries.Login;

public record LoginQuery(string Email, string Password) : IQuery<IResult<LoginQueryResult>>;

public record LoginQueryResult(string Token, string RefreshToken);