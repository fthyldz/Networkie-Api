using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Admin.Universities.Commands.DeleteUniversity;

public record DeleteUniversityCommand(Guid UniversityId) : ICommand<IResult<DeleteUniversityCommandResult>>;

public record DeleteUniversityCommandResult();