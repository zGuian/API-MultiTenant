using Microsoft.AspNetCore.Http;

namespace GearCore.Monolith.Infra.CC.Tenacy
{
    public class HttpTenantProvider(IHttpContextAccessor context) : ITenantProvider
    {
        private readonly IHttpContextAccessor _context = context;

        public string TenantId
        {
            get
            {
                if (_context.HttpContext?.Request.Headers.TryGetValue("X-Tenant-ID", out var headerTenant) == true)
                    return headerTenant!;

                var claim = _context.HttpContext?.User.Claims
                    .FirstOrDefault(c => c.Type == "tenant");

                if (claim != null)
                    return claim.Value;

                throw new Exception("Tenant não foi fornecido.");
            }
        }
    }
}
