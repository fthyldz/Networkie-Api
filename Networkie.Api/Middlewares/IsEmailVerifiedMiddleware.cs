using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;

namespace Networkie.Api.Middlewares;

public class IsEmailVerifiedMiddleware(RequestDelegate next, IMemoryCache memoryCache, IConfiguration configuration)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value;
        if (path != null && (path.StartsWith("/api/auth/complete-profile", StringComparison.OrdinalIgnoreCase) || path.StartsWith("/api/auth/resend-email-verification-code", StringComparison.OrdinalIgnoreCase) || path.StartsWith("/api/common/", StringComparison.OrdinalIgnoreCase)))
        {
            await next(context);
            return;
        }
        
        var user = context.User;

        if (user?.Identity?.IsAuthenticated == true)
        {
            var versionClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Version);

            if (versionClaim != null && bool.Parse(versionClaim.Value) != true)
            {
                var nameIdentifierClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var uniqueCode = Guid.NewGuid();
                memoryCache.Set(uniqueCode, Guid.Parse(nameIdentifierClaim!.Value), TimeSpan.FromDays(configuration.GetValue<int>("InMemoryCache:ExpirationInHours")));
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { IsEmailVerified = false, uniqueCode = uniqueCode.ToString() });
                return;
            }
        }

        await next(context);
    }
}