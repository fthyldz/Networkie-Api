using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Admin.MapInfo.Commands.CreateCountry;

public record CreateCountryCommand(string Name) : ICommand<IResult<CreateCountryCommandResult>>;

public record CreateCountryCommandResult();