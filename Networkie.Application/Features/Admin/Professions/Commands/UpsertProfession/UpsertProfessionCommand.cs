using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Admin.Professions.Commands.UpsertProfession;

public record UpsertProfessionCommand(Guid? Id, string Name) : ICommand<IResult<UpsertProfessionCommandResult>>;

public record UpsertProfessionCommandResult();