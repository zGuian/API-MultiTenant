namespace GearCore.Monolith.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class RequireTenantAttribute : Attribute
    {
    }
}
