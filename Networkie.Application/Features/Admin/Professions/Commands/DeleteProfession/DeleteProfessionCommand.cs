using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Admin.Professions.Commands.DeleteProfession;

public record DeleteProfessionCommand(Guid Id) : ICommand<IResult<DeleteProfessionCommandResult>>;

public record DeleteProfessionCommandResult();