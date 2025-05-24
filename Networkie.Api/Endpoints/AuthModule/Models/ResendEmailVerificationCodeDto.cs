using FluentValidation;

namespace Networkie.Api.Endpoints.AuthModule.Models;

public record ResendEmailVerificationCodeDto(Guid UniqueCode);

public class ResendEmailVerificationCodeDtoValidator : AbstractValidator<ResendEmailVerificationCodeDto>
{
    public ResendEmailVerificationCodeDtoValidator()
    {
        RuleFor(x => x.UniqueCode).NotEmpty().NotNull();
    }
}