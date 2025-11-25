namespace GearCore.Monolith.Core.Commons.Entities
{
    public class Tenant
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Subdomain { get; set; }
        public bool Active { get; set; } = true;
    }
}
