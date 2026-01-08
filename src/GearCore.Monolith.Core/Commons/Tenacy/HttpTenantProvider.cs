using GearCore.Monolith.Core.Commons.Tenacy.Interfaces;
using GearCore.Monolith.Core.Exceptions;
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
                {
                    if (!string.IsNullOrEmpty(headerTenant))
                        return headerTenant!;
                }

                // 2. Claims
                var claim = _context.HttpContext?.User.Claims
                    .FirstOrDefault(c => c.Type == "TenantId");

                if (claim != null)
                    return claim.Value;

                throw new NotFoundException("TenantId não encontrado HTTP");
            }
        }
    }
}
