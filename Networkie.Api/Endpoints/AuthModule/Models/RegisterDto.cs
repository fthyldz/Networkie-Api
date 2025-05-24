using FluentValidation;

namespace Networkie.Api.Endpoints.AuthModule.Models;

public record RegisterDto(string Email, string FirstName, string? MiddleName, string? LastName, string Password, bool Terms);

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
        RuleFor(x => x.FirstName).NotEmpty().NotNull();
        RuleFor(x => x.Password).NotEmpty().NotNull();
    }
}