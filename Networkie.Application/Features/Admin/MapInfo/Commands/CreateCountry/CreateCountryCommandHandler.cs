using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Entities;
using Networkie.Persistence.Abstractions;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Admin.MapInfo.Commands.CreateCountry;

public class CreateCountryCommandHandler(
    ICountryRepository countryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCountryCommand, IResult<CreateCountryCommandResult>>
{
    public async Task<IResult<CreateCountryCommandResult>> Handle(CreateCountryCommand command, CancellationToken cancellationToken)
    {
        var country = await countryRepository.GetByNameAsync(command.Name, cancellationToken);
        if (country != null)
            return ErrorResult<CreateCountryCommandResult, string>.Create("Ülke zaten mevcut.");
        
        var newCountry = new Country()
        {
            Name = command.Name
        };
        
        await countryRepository.AddAsync(newCountry, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return SuccessResult<CreateCountryCommandResult>.Create(new CreateCountryCommandResult());
    }
}