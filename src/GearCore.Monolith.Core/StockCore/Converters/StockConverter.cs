using GearCore.Monolith.Core.StockCore.DTOs;
using GearCore.Monolith.Core.StockCore.Entities;
using Mapster;

namespace GearCore.Monolith.Core.StockCore.Converters
{
    public static class StockConverter
    {
        [Obsolete("Não esta em uso", true)]
        public static void StockConverters()
        {
            TypeAdapterConfig<Stock, StockViewDto>.NewConfig()
                .ConstructUsing(e => new StockViewDto(e.TenantId, e.ProductId, e.Quantity, e.ReservedQuantity, e.Active));
        }
    }
}
