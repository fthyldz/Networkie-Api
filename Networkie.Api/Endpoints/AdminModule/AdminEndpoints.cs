using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Networkie.Api.Endpoints.AdminModule.Models;
using Networkie.Application.Features.Admin.Departments.Commands.DeleteDepartment;
using Networkie.Application.Features.Admin.Departments.Commands.UpsertDepartment;
using Networkie.Application.Features.Admin.Departments.Queries.Departments;
using Networkie.Application.Features.Admin.MapInfo.Commands.CreateCity;
using Networkie.Application.Features.Admin.MapInfo.Commands.CreateCountry;
using Networkie.Application.Features.Admin.MapInfo.Queries.Cities;
using Networkie.Application.Features.Admin.MapInfo.Queries.Countries;
using Networkie.Application.Features.Admin.Professions.Commands.DeleteProfession;
using Networkie.Application.Features.Admin.Professions.Commands.UpsertProfession;
using Networkie.Application.Features.Admin.Professions.Queries.Professions;
using Networkie.Application.Features.Admin.SocialPlatforms.Commands.DeleteSocialPlatform;
using Networkie.Application.Features.Admin.SocialPlatforms.Commands.UpsertSocialPlatform;
using Networkie.Application.Features.Admin.SocialPlatforms.Queries.SocialPlatforms;
using Networkie.Application.Features.Admin.Universities.Commands.DeleteUniversity;
using Networkie.Application.Features.Admin.Universities.Commands.UpsertUniversity;
using Networkie.Application.Features.Admin.Universities.Queries.Universities;
using Networkie.Application.Features.Admin.Users.Commands.DeleteUser;
using Networkie.Application.Features.Admin.Users.Queries.Users;
using Networkie.Application.Features.Profile.Commands.UpdateProfile;
using Networkie.Application.Features.Profile.Queries.GetUser;

namespace Networkie.Api.Endpoints.AdminModule;

public class AdminEndpoints : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroup = app.MapGroup("/admin").WithTags("Admin").RequireAuthorization("AdminOnly");
        
        var userGroup = routeGroup.MapGroup("/users").WithTags("Users");

        userGroup.MapGet("/", async ([AsParameters] UsersFilterDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new UsersQuery(request.PageIndex, request.PageSize,
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Email,
                request.PhoneNumber);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        userGroup.MapGet("/{userId:guid}", async (Guid userId, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new GetUserQuery(userId);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        userGroup.MapPut("/{userId:guid}",
            async (Guid userId, [FromBody] UpdateUserDto request, HttpContext httpContext, IMediator mediator,
                CancellationToken cancellationToken = default) =>
            {
                var command = new UpdateProfileCommand(
                    userId,
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
        
        userGroup.MapDelete("/{userId:guid}", async (Guid userId, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new DeleteUserCommand(userId);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        var universityGroup = routeGroup.MapGroup("/universities").WithTags("Universities");

        universityGroup.MapGet("/", async ([AsParameters] UniversitiesFilterDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new UniversitiesQuery(request.PageIndex, request.PageSize, request.Search);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        universityGroup.MapPost("/", async ([FromBody] UpsertUniversityDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var command = new UpsertUniversityCommand(null, request.Name);
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        });
        
        universityGroup.MapPut("/{universityId:guid}", async (Guid universityId, [FromBody] UpsertUniversityDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var command = new UpsertUniversityCommand(universityId, request.Name);
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        });
        
        universityGroup.MapDelete("/{universityId:guid}", async (Guid universityId, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var command = new DeleteUniversityCommand(universityId);
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        });
        
        
        var departmentGroup = routeGroup.MapGroup("/departments").WithTags("Departments");

        departmentGroup.MapGet("/", async ([AsParameters] DepartmentsFilterDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new DepartmentsQuery(request.PageIndex, request.PageSize, request.Search);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        departmentGroup.MapPost("/", async ([FromBody] UpsertDepartmentDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var command = new UpsertDepartmentCommand(null, request.Name);
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        });
        
        departmentGroup.MapPut("/{departmentId:guid}", async (Guid departmentId, [FromBody] UpsertDepartmentCommand request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var command = new UpsertDepartmentCommand(departmentId, request.Name);
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        });
        
        departmentGroup.MapDelete("/{departmentId:guid}", async (Guid departmentId, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var command = new DeleteDepartmentCommand(departmentId);
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        });
        
        
        var professionGroup = routeGroup.MapGroup("/professions").WithTags("Professions");

        professionGroup.MapGet("/", async ([AsParameters] ProfessionsFilterDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new ProfessionsQuery(request.PageIndex, request.PageSize, request.Search);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        professionGroup.MapPost("/", async ([FromBody] UpsertProfessionDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var command = new UpsertProfessionCommand(null, request.Name);
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        });
        
        professionGroup.MapPut("/{professionId:guid}", async (Guid professionId, [FromBody] UpsertProfessionDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var command = new UpsertProfessionCommand(professionId, request.Name);
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        });
        
        professionGroup.MapDelete("/{professionId:guid}", async (Guid professionId, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var command = new DeleteProfessionCommand(professionId);
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        });
        
        
        var socialPlatformGroup = routeGroup.MapGroup("/social-platforms").WithTags("Social Platforms");

        socialPlatformGroup.MapGet("/", async ([AsParameters] SocialPlatformsFilterDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new SocialPlatformsQuery(request.PageIndex, request.PageSize, request.Search);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        socialPlatformGroup.MapPost("/", async ([FromBody] UpsertSocialPlatformDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var command = new UpsertSocialPlatformCommand(null, request.Name, request.IsRequired);
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        });
        
        socialPlatformGroup.MapPut("/{socialPlatformId:guid}", async (Guid socialPlatformId, [FromBody] UpsertSocialPlatformDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var command = new UpsertSocialPlatformCommand(socialPlatformId, request.Name, request.IsRequired);
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        });
        
        socialPlatformGroup.MapDelete("/{socialPlatformId:guid}", async (Guid socialPlatformId, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var command = new DeleteSocialPlatformCommand(socialPlatformId);
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        });
        
        var mapInfoGroup = routeGroup.MapGroup("/map-info").WithTags("Map Info");

        mapInfoGroup.MapGet("/countries", async ([AsParameters] CountriesFilterDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new CountriesQuery(request.PageIndex, request.PageSize, request.Search);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        mapInfoGroup.MapPost("/countries", async ([FromBody] InsertCountryDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var command = new CreateCountryCommand(request.Name);
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        });
        
        mapInfoGroup.MapGet("/cities", async ([AsParameters] CitiesFilterDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new CitiesQuery(request.PageIndex, request.PageSize, request.SearchCity, request.SearchCountry);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        mapInfoGroup.MapPost("/cities", async ([FromBody] InsertCityDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var command = new CreateCityCommand(request.Name, request.Country);
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        });
    }
}