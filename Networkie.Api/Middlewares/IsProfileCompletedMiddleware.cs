using System.Security.Claims;

namespace Networkie.Api.Middlewares;

public class IsProfileCompletedMiddleware(RequestDelegate next)
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
            var anonymousClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Anonymous);

            if (anonymousClaim != null && bool.Parse(anonymousClaim.Value) != true)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { IsProfileCompleted = false });
                return;
            }
        }

        await next(context);
    }
}