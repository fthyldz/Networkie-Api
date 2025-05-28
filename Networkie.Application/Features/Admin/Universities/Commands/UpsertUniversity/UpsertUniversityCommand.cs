using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Admin.Universities.Commands.UpsertUniversity;

public record UpsertUniversityCommand(Guid? Id, string Name) : ICommand<IResult<UpsertUniversityCommandResult>>;

public record UpsertUniversityCommandResult();