using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Core.TenantCore.Entities;

namespace GearCore.Monolith.Infra.Data.ProductInfra.Seed
{
    internal static class ProductSeed
    {
        public static IEnumerable<Product> GetSeeds()
        {
            var tenantId1 = "24eed906-45be-4d57-a056-c7c480b9c915";
            var tenantId2 = "4213458c-9554-4de5-998e-ca06ed257749";
            var tenantId = "d5fffc28-f956-4c85-92bf-b6d8c961bb11";

            var products = new List<Product>
            {
                new(
                    tenantId: tenantId1,
                    name: "Smartphone Galaxy X",
                    description: "Smartphone com tela AMOLED de 6.5 polegadas e 128GB de armazenamento",
                    price: 2999.90m,
                    isActive: true,
                    brand: "Samsung"
                ),

                new(
                    tenantId: tenantId1,
                    name: "Notebook Aspire 5",
                    description: "Notebook com processador Intel i5, 8GB de RAM e SSD de 512GB",
                    price: 4299.00m,
                    isActive: true,
                    brand: "Acer"
                ),

                new(
                    tenantId: tenantId1,
                    name: "Smart TV 50 4K UHD",
                    description: "Smart TV de 50 polegadas com resolução 4K, HDR e apps integrados",
                    price: 2799.99m,
                    isActive: true,
                    brand: "LG"
                ),

                new(
                    tenantId: tenantId1,
                    name: "Fone de Ouvido Bluetooth",
                    description: "Fone de ouvido sem fio com cancelamento ativo de ruído",
                    price: 899.90m,
                    isActive: true,
                    brand: "Sony"
                ),

                new(
                    tenantId: tenantId1,
                    name: "Mouse Gamer RGB",
                    description: "Mouse gamer com sensor óptico de alta precisão e iluminação RGB",
                    price: 199.90m,
                    isActive: true,
                    brand: "Logitech"
                ),

                new(

                    tenantId: tenantId2,
                    name: "Arroz Tipo 1 - 5kg",
                    description: "Arroz branco tipo 1, pacote de 5kg",
                    price: 29.90m,
                    isActive: true,
                    brand: "Tio João"
                ),

                new(
                    tenantId: tenantId2,
                    name: "Feijão Carioca - 1kg",
                    description: "Feijão carioca selecionado, pacote de 1kg",
                    price: 8.99m,
                    isActive: true,
                    brand: "Camil"
                ),

                new(
                    tenantId: tenantId2,
                    name: "Açúcar Refinado - 1kg",
                    description: "Açúcar refinado branco, pacote de 1kg",
                    price: 4.79m,
                    isActive: true,
                    brand: "União"
                ),

                new(
                    tenantId: tenantId2,
                    name: "Óleo de Soja - 900ml",
                    description: "Óleo de soja refinado para preparo de alimentos",
                    price: 7.49m,
                    isActive: true,
                    brand: "Liza"
                ),

                new(

                    tenantId: tenantId2,
                    name: "Café Torrado e Moído - 500g",
                    description: "Café tradicional torrado e moído",
                    price: 16.90m,
                    isActive: true,
                    brand: "Pilão"
                ),

                new(
                    tenantId: tenantId,
                    name: "Vaso Decorativo Cerâmica",
                    description: "Vaso decorativo em cerâmica branca com acabamento fosco",
                    price: 129.90m,
                    isActive: true,
                    brand: "Casa Moderna"
                ),

                new(
                    tenantId: tenantId,
                    name: "Quadro Decorativo Abstrato",
                    description: "Quadro decorativo com arte abstrata em tons neutros",
                    price: 189.90m,
                    isActive: true,
                    brand: "Arte & Estilo"
                ),

                new(
                    tenantId: tenantId,
                    name: "Luminária de Mesa",
                    description: "Luminária de mesa com base em madeira e cúpula de tecido",
                    price: 219.00m,
                    isActive: true,
                    brand: "LumiArt"
                ),

                new(
                    tenantId: tenantId,
                    name: "Espelho Decorativo Redondo",
                    description: "Espelho decorativo redondo com moldura metálica dourada",
                    price: 259.90m,
                    isActive: true,
                    brand: "Reflex Decor"
                ),

                new(
                    tenantId: tenantId,
                    name: "Almofada Decorativa",
                    description: "Almofada decorativa em tecido linho com estampa geométrica",
                    price: 79.90m,
                    isActive: true,
                    brand: "DecorLar"
                )
            };
            return products;
        }

        public static IEnumerable<Product> GetSeeds(Tenant[] tenants)
        {
            const string tenantId1 = "24eed906-45be-4d57-a056-c7c480b9c915";
            const string tenantId2 = "4213458c-9554-4de5-998e-ca06ed257749";
            const string tenantId3 = "d5fffc28-f956-4c85-92bf-b6d8c961bb11";

            var products = new Product[]
            {
                new(
                    tenantId: tenantId1,
                    name: "Smartphone Galaxy X",
                    description: "Smartphone com tela AMOLED de 6.5 polegadas e 128GB de armazenamento",
                    price: 2999.90m,
                    isActive: true,
                    brand: "Samsung"
                ),

                new(
                    tenantId: tenantId1,
                    name: "Notebook Aspire 5",
                    description: "Notebook com processador Intel i5, 8GB de RAM e SSD de 512GB",
                    price: 4299.00m,
                    isActive: true,
                    brand: "Acer"
                ),

                new(
                    tenantId: tenantId1,
                    name: "Smart TV 50 4K UHD",
                    description: "Smart TV de 50 polegadas com resolução 4K, HDR e apps integrados",
                    price: 2799.99m,
                    isActive: true,
                    brand: "LG"
                ),

                new(
                    tenantId: tenantId1,
                    name: "Fone de Ouvido Bluetooth",
                    description: "Fone de ouvido sem fio com cancelamento ativo de ruído",
                    price: 899.90m,
                    isActive: true,
                    brand: "Sony"
                ),

                new(
                    tenantId: tenantId1,
                    name: "Mouse Gamer RGB",
                    description: "Mouse gamer com sensor óptico de alta precisão e iluminação RGB",
                    price: 199.90m,
                    isActive: true,
                    brand: "Logitech"
                ),

                new(

                    tenantId: tenantId2,
                    name: "Arroz Tipo 1 - 5kg",
                    description: "Arroz branco tipo 1, pacote de 5kg",
                    price: 29.90m,
                    isActive: true,
                    brand: "Tio João"
                ),

                new(
                    tenantId: tenantId2,
                    name: "Feijão Carioca - 1kg",
                    description: "Feijão carioca selecionado, pacote de 1kg",
                    price: 8.99m,
                    isActive: true,
                    brand: "Camil"
                ),

                new(
                    tenantId: tenantId2,
                    name: "Açúcar Refinado - 1kg",
                    description: "Açúcar refinado branco, pacote de 1kg",
                    price: 4.79m,
                    isActive: true,
                    brand: "União"
                ),

                new(
                    tenantId: tenantId2,
                    name: "Óleo de Soja - 900ml",
                    description: "Óleo de soja refinado para preparo de alimentos",
                    price: 7.49m,
                    isActive: true,
                    brand: "Liza"
                ),

                new(

                    tenantId: tenantId2,
                    name: "Café Torrado e Moído - 500g",
                    description: "Café tradicional torrado e moído",
                    price: 16.90m,
                    isActive: true,
                    brand: "Pilão"
                ),

                new(
                    tenantId: tenantId3,
                    name: "Vaso Decorativo Cerâmica",
                    description: "Vaso decorativo em cerâmica branca com acabamento fosco",
                    price: 129.90m,
                    isActive: true,
                    brand: "Casa Moderna"
                ),

                new(
                    tenantId: tenantId3,
                    name: "Quadro Decorativo Abstrato",
                    description: "Quadro decorativo com arte abstrata em tons neutros",
                    price: 189.90m,
                    isActive: true,
                    brand: "Arte & Estilo"
                ),

                new(
                    tenantId: tenantId3,
                    name: "Luminária de Mesa",
                    description: "Luminária de mesa com base em madeira e cúpula de tecido",
                    price: 219.00m,
                    isActive: true,
                    brand: "LumiArt"
                ),

                new(
                    tenantId: tenantId3,
                    name: "Espelho Decorativo Redondo",
                    description: "Espelho decorativo redondo com moldura metálica dourada",
                    price: 259.90m,
                    isActive: true,
                    brand: "Reflex Decor"
                ),

                new(
                    tenantId: tenantId3,
                    name: "Almofada Decorativa",
                    description: "Almofada decorativa em tecido linho com estampa geométrica",
                    price: 79.90m,
                    isActive: true,
                    brand: "DecorLar"
                )
            };

            foreach (var item in products)
            {
                item.Tenant = tenants.Where(t => t.Id == item.TenantId).Single();
            }

            return products;
        }
    }
}
