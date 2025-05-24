using System.Security.Claims;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Networkie.Api.Endpoints.AuthModule.Models;
using Networkie.Api.Endpoints.ProfileModule.Models;
using Networkie.Application.Features.Auth.Commands.CompleteProfile;
using Networkie.Application.Features.Profile.Commands.UpdateProfile;
using Networkie.Application.Features.Profile.Queries.GetUser;

namespace Networkie.Api.Endpoints.ProfileModule;

public class ProfileEndpoints : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroup = app.MapGroup("/profile").WithTags("Profile");

        routeGroup.MapGet("/account",
            async (HttpContext httpContext, IMediator mediator, CancellationToken cancellationToken = default) =>
            {
                var nameIdentifierClaim =
                    httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var query = new GetUserQuery(Guid.Parse(nameIdentifierClaim!.Value));
                var response = await mediator.Send(query, cancellationToken);
                return Results.Ok(response);
            });
        
        routeGroup.MapPut("/update",
            async ([FromBody] UpdateProfileDto request, HttpContext httpContext, IMediator mediator,
                CancellationToken cancellationToken = default) =>
            {
                var nameIdentifierClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                
                var command = new UpdateProfileCommand(
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
                    request.Universities.Select(u => new UpdateProfileCommandUniversity(u.University, u.UniversityId, u.Department, u.DepartmentId, u.EntryYear)).ToList(),
                    request.Country,
                    request.CountryId,
                    request.State,
                    request.StateId,
                    request.City,
                    request.CityId,
                    request.District,
                    request.DistrictId,
                    request.SocialPlatforms.Select(sp => new UpdateProfileCommandSocialPlatform(sp.SocialPlatformId, sp.Url)).ToList()
                );
                var response = await mediator.Send(command, cancellationToken);
                return Results.Ok(response);
            });
    }
}