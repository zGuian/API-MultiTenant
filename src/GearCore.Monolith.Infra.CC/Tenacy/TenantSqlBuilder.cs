namespace GearCore.Monolith.Infra.CC.Tenacy
{
    public class TenantSqlBuilder(ITenantProvider tenantProvider)
    {
        private readonly ITenantProvider _tenantProvider = tenantProvider;

        public (string sql, object parameters) AddTenantFilter(string sql, object parameters)
        {
            var dict = new Dictionary<string, string>
            {
                { "TenantId", _tenantProvider.TenantId }
            };

            if (!sql.Contains("WHERE", StringComparison.InvariantCultureIgnoreCase))
                sql += " WHERE TenantId = @TenantId";
            else
                sql += " AND TenantId = @TenantId";

            return (sql, dict);
        }
    }
}
