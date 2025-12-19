using GearCore.Monolith.Core.ProductCore.DTOs;
using GearCore.Monolith.Core.ProductCore.Entities;
using Mapster;

namespace GearCore.Monolith.Core.ProductCore.Converters
{
    public static class ProductConverter
    {
        [Obsolete("Não esta em uso")]
        public static void ProductConverters()
        {
            TypeAdapterConfig<ProductUpdateDto, Product>.NewConfig()
                .ConstructUsing(dto => new Product(dto.Name, dto.Description, dto.Price, dto.IsActive, dto.Brand));
        }
    }
}
