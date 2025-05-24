using FluentValidation;

namespace Networkie.Api.Endpoints.AuthModule.Models;

public record LoginDto(string Email, string Password);

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().NotNull();
    }
}