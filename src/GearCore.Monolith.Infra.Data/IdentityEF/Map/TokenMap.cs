using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GearCore.Monolith.Infra.Data.IdentityEF.Map
{
    public class TokenMap : IEntityTypeConfiguration<ApplicationToken>
    {
        public void Configure(EntityTypeBuilder<ApplicationToken> builder)
        {
            throw new NotImplementedException();
        }
    }
}
