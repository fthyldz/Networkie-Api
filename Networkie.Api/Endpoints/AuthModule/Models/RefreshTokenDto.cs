using FluentValidation;

namespace Networkie.Api.Endpoints.AuthModule.Models;

public record RefreshTokenDto(string RefreshToken);

public class RefreshTokenDtoValidator : AbstractValidator<RefreshTokenDto>
{
    public RefreshTokenDtoValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty().NotNull();
    }
}