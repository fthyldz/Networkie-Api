namespace Networkie.Api.Endpoints.AdminModule.Models;

public record UpsertSocialPlatformDto(Guid? Id, string Name, bool IsRequired);