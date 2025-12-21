using GearCore.Monolith.Core.SalesCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GearCore.Monolith.Infra.Data.SalesInfra.Map
{
    internal class SalesMap : IEntityTypeConfiguration<Sales>
    {
        public void Configure(EntityTypeBuilder<Sales> builder)
        {
            throw new NotImplementedException();
        }
    }
}
