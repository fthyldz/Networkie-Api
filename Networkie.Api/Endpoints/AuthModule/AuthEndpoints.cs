using System.Security.Claims;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Networkie.Api.Endpoints.AuthModule.Models;
using Networkie.Application.Features.Auth.Commands.CompleteProfile;
using Networkie.Application.Features.Auth.Commands.Register;
using Networkie.Application.Features.Auth.Commands.ResendEmailVerificationCode;
using Networkie.Application.Features.Auth.Commands.VerifyEmail;
using Networkie.Application.Features.Auth.Queries.Login;
using Networkie.Application.Features.Auth.Queries.ProfileCompleted;

namespace Networkie.Api.Endpoints.AuthModule;

public class AuthEndpoints : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroup = app.MapGroup("/auth").WithTags("Auth");

        routeGroup.AllowAnonymous().MapPost("/login",
            async ([FromBody] LoginDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
            {
                var query = new LoginQuery(request.Email, request.Password);
                var response = await mediator.Send(query, cancellationToken);
                return Results.Ok(response);
            });
        
        routeGroup.MapPost("/refresh-token",
            async ([FromBody] ResendEmailVerificationCodeDto request, IMediator mediator,
                CancellationToken cancellationToken = default) =>
            {
                var command = new ResendEmailVerificationCodeCommand(request.UniqueCode);
                var response = await mediator.Send(command, cancellationToken);
                return Results.Ok(response);
            });

        routeGroup.AllowAnonymous().MapPost("/register",
            async ([FromBody] RegisterDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
            {
                var command = new RegisterCommand(request.Email, request.FirstName, request.MiddleName,
                    request.LastName, request.Password);
                var response = await mediator.Send(command, cancellationToken);
                return Results.Ok(response);
            });

        routeGroup.MapPost("/verify-email",
            async ([FromBody] VerifyEmailDto request, IMediator mediator,
                CancellationToken cancellationToken = default) =>
            {
                var command = new VerifyEmailCommand(request.VerificationCode);
                var response = await mediator.Send(command, cancellationToken);
                return Results.Ok(response);
            }).AllowAnonymous();
        
        routeGroup.MapPost("/resend-email-verification-code",
            async ([FromBody] ResendEmailVerificationCodeDto request, IMediator mediator,
                CancellationToken cancellationToken = default) =>
            {
                var command = new ResendEmailVerificationCodeCommand(request.UniqueCode);
                var response = await mediator.Send(command, cancellationToken);
                return Results.Ok(response);
            });

        routeGroup.MapGet("/profile-completed", async ([FromQuery] Guid userId, IMediator meidator, CancellationToken cancellationToken = default) =>
        {
            var query = new ProfileCompletedQuery(userId);
            var response = await meidator.Send(query, cancellationToken);
            return Results.Ok(response);
        });

        routeGroup.MapPost("/complete-profile",
            async ([FromBody] CompleteProfileDto request, HttpContext httpContext, IMediator mediator,
                CancellationToken cancellationToken = default) =>
            {
                var nameIdentifierClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                
                var command = new CompleteProfileCommand(
                    Guid.Parse(nameIdentifierClaim!.Value),
                    request.PhoneCountryCode,
                    request.PhoneNumber,
                    request.Gender,
                    request.BirthOfDate,
                    request.IsEmployed,
                    request.IsSeekingForJob,
                    request.IsHiring,
                    request.Profession,
                    request.ProfessionId,
                    request.Universities.Select(u => new CompleteProfileCommandUniversity(u.University, u.UniversityId, u.Department, u.DepartmentId, u.EntryYear)).ToList(),
                    request.Country,
                    request.CountryId,
                    request.State,
                    request.StateId,
                    request.City,
                    request.CityId,
                    request.District,
                    request.DistrictId,
                    request.SocialPlatforms.Select(sp => new CompleteProfileCommandSocialPlatform(sp.SocialPlatformId, sp.Url)).ToList()
                );
                var response = await mediator.Send(command, cancellationToken);
                return Results.Ok(response);
            });
    }
}