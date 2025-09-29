using System.Text;
using System.Text.Encodings.Web;
using TaskApp.Api.Services;

namespace TaskApp.Api.Middleware
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public BasicAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Skip authentication for health checks, swagger, or login endpoint
            if (context.Request.Path.StartsWithSegments("/swagger") || 
                context.Request.Path.StartsWithSegments("/health") ||
                context.Request.Path.StartsWithSegments("/api/auth/login"))
            {
                await _next(context);
                return;
            }

            string? authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                var encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
                var username = decodedUsernamePassword.Split(':')[0];
                var password = decodedUsernamePassword.Split(':')[1];

                // Get the service from the request scope
                var authService = context.RequestServices.GetRequiredService<BasicAuthService>();
                
                if (authService.ValidateCredentials(username, password))
                {
                    await _next(context);
                    return;
                }
            }

            context.Response.Headers["WWW-Authenticate"] = "Basic";
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
        }
    }
}
