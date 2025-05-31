using FluentValidation;

namespace Networkie.Api.Endpoints.AdminModule.Models;


public record UsersFilterDto(int PageIndex = 0, int PageSize = 25,
    string? FirstName = null,
    string? MiddleName = null,
    string? LastName = null,
    string? Email = null,
    string? PhoneNumber = null,
    string? Role = null);

public class UsersFilterDtoValidator : AbstractValidator<UsersFilterDto>
{
    public UsersFilterDtoValidator()
    {
        RuleFor(x => x.PageIndex).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(x => x.PageSize).NotEmpty().GreaterThanOrEqualTo(25).LessThanOrEqualTo(100);
    }
}