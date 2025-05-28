using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Features.Admin.MapInfo.Commands.CreateCity;

public record CreateCityCommand(string Name, string Country) : ICommand<IResult<CreateCityCommandResult>>;

public record CreateCityCommandResult();