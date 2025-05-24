using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Auth.Queries.ProfileCompleted;

public record ProfileCompletedQuery(Guid UserId) : IQuery<IResult<ProfileCompletedQueryResult>>;

public record ProfileCompletedQueryResult(bool IsCompleted);