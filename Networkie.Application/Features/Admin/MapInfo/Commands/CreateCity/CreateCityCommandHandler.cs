using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Entities;
using Networkie.Persistence.Abstractions;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Admin.MapInfo.Commands.CreateCity;

public class CreateCityCommandHandler(
    ICountryRepository countryRepository,
    IStateRepository stateRepository,
    ICityRepository cityRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCityCommand, IResult<CreateCityCommandResult>>
{
    public async Task<IResult<CreateCityCommandResult>> Handle(CreateCityCommand command, CancellationToken cancellationToken)
    {
        var city = await cityRepository.GetByContainsNameAsync(command.Name, cancellationToken);
        if (city.Any())
            return ErrorResult<CreateCityCommandResult, string>.Create("Şehir zaten mevcut.");
        
        var country = await countryRepository.GetByNameAsync(command.Country, cancellationToken);
        if (country == null)
        {
            var newCountry = new Country()
            {
                Name = command.Name
            };
        
            await countryRepository.AddAsync(newCountry, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            country = newCountry;
        }
        
        
        var state = await stateRepository.GetByCountryIdAndNameAsync(country.Id, command.Name, cancellationToken);
        if (state == null)
        {
            var newState = new State()
            {
                CountryId = country.Id,
                Name = command.Name
            };
            await stateRepository.AddAsync(newState, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            state = newState;
        }
        
        var newCity = new City()
        {
            CountryId = country.Id,
            StateId = state.Id,
            Name = command.Name
        };
        
        await cityRepository.AddAsync(newCity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return SuccessResult<CreateCityCommandResult>.Create(new CreateCityCommandResult());
    }
}