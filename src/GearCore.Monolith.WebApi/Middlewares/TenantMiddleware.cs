namespace GearCore.Monolith.WebApi.Middlewares
{
    public class TenantMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            string? tenantId = null;

            if (context.Request.Headers.TryGetValue("X-Tenant-ID", out var headerTenantId))
                tenantId = headerTenantId.ToString();

            if (tenantId == null)
                tenantId = context.User.FindFirst("tenant")?.Value;

            if (tenantId == null)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("TenantId is missing.");
                return;
            }

            context.Items["TenantId"] = tenantId;
            await _next(context);
        }
    }
}
