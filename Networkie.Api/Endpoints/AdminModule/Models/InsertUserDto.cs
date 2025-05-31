using FluentValidation;

namespace Networkie.Api.Endpoints.AdminModule.Models;

public record InsertUserDto(string Email, string FirstName, string? MiddleName, string? LastName, string Password, string Role);


public class InsertUserDtoValidator : AbstractValidator<InsertUserDto>
{
    public InsertUserDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
        RuleFor(x => x.FirstName).NotEmpty().NotNull();
        RuleFor(x => x.Password).NotEmpty().NotNull();
    }
}