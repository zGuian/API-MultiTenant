using GearCore.Monolith.WebApi.Attributes;

namespace GearCore.Monolith.WebApi.Middlewares
{
    public class TenantMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();

            if (endpoint?.Metadata.GetMetadata<RequireTenantAttribute>() == null)
            {
                await _next(context);
                return;
            }

            string? tenantId = null;

            if (context.User.Identity?.IsAuthenticated == true)
            {
                tenantId = context.User.FindFirst("TenantId")?.Value;
            }

            if (tenantId == null)
            {
                if (context.Request.Headers.TryGetValue("X-Tenant-ID", out var headerTenantId))
                    tenantId = headerTenantId.ToString();
            }

            if (tenantId == null)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("TenantId is missing.");
                return;
            }

            context.Items["TenantID"] = tenantId;
            await _next(context);
        }
    }
}
