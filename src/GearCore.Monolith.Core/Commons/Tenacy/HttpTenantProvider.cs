using GearCore.Monolith.Core.Commons.Tenacy.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GearCore.Monolith.Core.Commons.Tenacy
{
    public class HttpTenantProvider(IHttpContextAccessor context) : ITenantProvider
    {
        private readonly IHttpContextAccessor _context = context;

        public string TenantId
        {
            get
            {
                // 1. Header
                if (_context.HttpContext?.Request.Headers.TryGetValue("X-Tenant-ID", out var headerTenant) == true)
                    return headerTenant!;

                // 2. Claims
                var claim = _context.HttpContext?.User.Claims
                    .FirstOrDefault(c => c.Type == "tenant");

                if (claim != null)
                    return claim.Value;

                // 3. Design-time (EF migrations)
                // HttpContext == null → MIGRATION MODE
                return "master"; // <-- TENANT DEFAULT
            }
        }
    }
}
