using Networkie.Application.Abstractions.Cqrs;
using Networkie.Application.Abstractions.Results;
using Networkie.Application.Common.Results;
using Networkie.Entities;
using Networkie.Persistence.Abstractions;
using Networkie.Persistence.Abstractions.Repositories;

namespace Networkie.Application.Features.Auth.Commands.CompleteProfile;

public class CompleteProfileCommandHandler(
    IUserRepository userRepository,
    IProfessionRepository professionRepository,
    IUniversityRepository universityRepository,
    IDepartmentRepository departmentRepository,
    ICountryRepository countryRepository,
    IStateRepository stateRepository,
    ICityRepository cityRepository,
    IDistrictRepository districtRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CompleteProfileCommand, IResult<CompleteProfileCommandResult>>
{
    public async Task<IResult<CompleteProfileCommandResult>> Handle(CompleteProfileCommand command, CancellationToken cancellationToken)
    {
        if (command.UserId == Guid.Empty)
            return ErrorResult<CompleteProfileCommandResult, string>.Create("Kullanıcı bulunamadı.");

        var user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user == null)
            return ErrorResult<CompleteProfileCommandResult, string>.Create("Kullanıcı bulunamadı.");

        user.PhoneCountryCode = command.PhoneCountryCode;
        user.PhoneNumber = command.PhoneNumber;
        user.Gender = command.Gender;
        user.BirthOfDate = DateTime.SpecifyKind(command.BirthOfDate, DateTimeKind.Utc);
        user.IsEmployed = command.IsEmployed;
        user.IsSeekingForJob = command.IsSeekingForJob;
        user.IsHiring = command.IsHiring;
        
        
        #region Profession

        if (command.ProfessionId == null && !string.IsNullOrWhiteSpace(command.Profession))
        {
            var profession = await professionRepository.GetByNameAsync(command.Profession, cancellationToken);
            if (profession != null)
                user.ProfessionId = profession.Id;
            else
            {
                var newProfession = new Profession()
                {
                    Name = command.Profession
                };
                await professionRepository.AddAsync(newProfession, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                user.ProfessionId = newProfession.Id;
            }
        }
        else
        {
            user.ProfessionId = command.ProfessionId;
        }
        
        #endregion
        
        
        #region Universities

        foreach (var universityItem in command.Universities)
        {
            #region University
            
            University? userUniversity = null;
            if (universityItem.UniversityId == null && !string.IsNullOrWhiteSpace(universityItem.University))
            {
                var university = await universityRepository.GetByNameAsync(universityItem.University, cancellationToken);
                if (university != null)
                {
                    userUniversity = new University()
                    {
                        Id = university.Id,
                        Name = university.Name
                    };
                }
                else
                {
                    var newUniversity = new University()
                    {
                        Name = universityItem.University
                    };
                    await universityRepository.AddAsync(newUniversity, cancellationToken);
                    await unitOfWork.SaveChangesAsync(cancellationToken);
                    userUniversity = new University()
                    {
                        Id = newUniversity.Id,
                        Name = newUniversity.Name
                    };
                }
            }
            else
            {
                userUniversity = new University()
                {
                    Id = universityItem.UniversityId!.Value
                };
            }
            
            #endregion
            
            #region Department
            
            Department? userDepartment = null;
            if (universityItem.DepartmentId == null && !string.IsNullOrWhiteSpace(universityItem.Department))
            {
                var department = await departmentRepository.GetByNameAsync(universityItem.Department, cancellationToken);
                if (department != null)
                {
                    userDepartment = new Department()
                    {
                        Id = department.Id,
                        Name = department.Name
                    };
                }
                else
                {
                    var newDepartment = new Department()
                    {
                        Name = universityItem.Department
                    };
                    await departmentRepository.AddAsync(newDepartment, cancellationToken);
                    await unitOfWork.SaveChangesAsync(cancellationToken);
                    userDepartment = new Department()
                    {
                        Id = newDepartment.Id,
                        Name = newDepartment.Name
                    };
                }
            }
            else
            {
                userDepartment = new Department()
                {
                    Id = universityItem.DepartmentId!.Value
                };
            }
            
            #endregion
            
            #region University-Department

            user.UserUniversities.Add(new UserUniversity()
            {
                UniversityId = userUniversity!.Id,
                DepartmentId = userDepartment!.Id,
                EntryYear = universityItem.EntryYear,
            });
            
            
            #endregion
        }
        
        #endregion

        
        #region Country

        if (command.CountryId == null && !string.IsNullOrWhiteSpace(command.Country))
        {
            var country = await countryRepository.GetByNameAsync(command.Country, cancellationToken);
            if (country != null)
                user.CountryId = country.Id;
            else
            {
                var newCountry = new Country()
                {
                    Name = command.Country
                };
                await countryRepository.AddAsync(newCountry, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                user.CountryId = newCountry.Id;
            }
        }
        else
        {
            user.CountryId = command.CountryId;
        }

        #endregion
        
        #region State

        if (command.StateId == null && !string.IsNullOrWhiteSpace(command.State))
        {
            var state = await stateRepository.GetByCountryIdAndNameAsync(user.CountryId!.Value, command.State, cancellationToken);
            if (state != null)
                user.StateId = state.Id;
            else
            {
                var newState = new State()
                {
                    CountryId = user.CountryId!.Value,
                    Name = command.State
                };
                await stateRepository.AddAsync(newState, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                user.StateId = newState.Id;
            }
        }
        else
        {
            user.StateId = command.StateId;
        }

        #endregion
        
        #region City

        if (command.CityId == null && !string.IsNullOrWhiteSpace(command.City))
        {
            var city = await cityRepository.GetByCountryIdOrStateIdAndNameAsync(user.CountryId!.Value, user.StateId, command.City, cancellationToken);
            if (city != null)
                user.CityId = city.Id;
            else
            {
                var newCity = new City()
                {
                    CountryId = user.CountryId!.Value,
                    StateId = user.StateId,
                    Name = command.City
                };
                await cityRepository.AddAsync(newCity, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                user.CityId = newCity.Id;
            }
        }
        else
        {
            user.CityId = command.CityId;
        }

        #endregion
        
        #region District

        if (command.DistrictId == null && !string.IsNullOrWhiteSpace(command.District))
        {
            var district = await districtRepository.GetByCityIdAndNameAsync(user.CityId!.Value, command.District, cancellationToken);
            if (district != null)
                user.DistrictId = district.Id;
            else
            {
                var newDistrict = new District()
                {
                    CityId = user.CityId!.Value,
                    Name = command.District
                };
                await districtRepository.AddAsync(newDistrict, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                user.DistrictId = newDistrict.Id;
            }
        }
        else
        {
            user.DistrictId = command.DistrictId;
        }

        #endregion

        
        #region SocialPlatforms

        foreach (var socialPlatformItem in command.SocialPlatforms)
        {
            if (!string.IsNullOrWhiteSpace(socialPlatformItem.Url))
            {
                user.UserSocialPlatforms.Add(new UserSocialPlatform()
                {
                    SocialPlatformId = socialPlatformItem.SocialPlatformId,
                    Url = socialPlatformItem.Url
                });
            }
        }
        
        #endregion
        
        user.IsProfileCompleted = true;

        try
        {
            userRepository.Update(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        

        return SuccessResult<CompleteProfileCommandResult>.Create(new CompleteProfileCommandResult());
    }
}