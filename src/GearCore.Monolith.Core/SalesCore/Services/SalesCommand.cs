using GearCore.Monolith.Core.Exceptions;
using GearCore.Monolith.Core.ProductCore.Interfaces.Repositories;
using GearCore.Monolith.Core.SalesCore.DTOs.Requests;
using GearCore.Monolith.Core.SalesCore.Entities;
using GearCore.Monolith.Core.SalesCore.Interfaces.Repositories;
using GearCore.Monolith.Core.SalesCore.Interfaces.Services;

namespace GearCore.Monolith.Core.SalesCore.Services
{
    public class SalesCommand(ISalesCommandRepository saleCommandRepository
        , ISalesQueryRepository saleQueryRepository
        , IProductQueryRepository productQueryRepository) : ISalesCommand
    {
        private readonly ISalesCommandRepository _saleCommandRepository = saleCommandRepository;
        private readonly ISalesQueryRepository _saleQueryRepository = saleQueryRepository;
        private readonly IProductQueryRepository _productQueryRepository = productQueryRepository;

        public async Task ExecuteOrderAsync(RealizeSaleRequest request)
        {
            //PONTO DE ROUND-TRIP (MELHORAR DEPOIS)
            var products = await _productQueryRepository.GetManyProductById(request.Items.Select(rs => rs.ProductId));

            var saleItems = new List<SaleItem>();
            foreach (var item in request.Items)
            {
                var product = products.SingleOrDefault(p => p.Id == item.ProductId);
                if (product == null)
                {
                    continue;
                }
                saleItems.Add(new SaleItem(product, item.Quantity));
            }

            var sale = new Sales(saleItems, request.UserId);
            await _saleCommandRepository.ExecuteSaleAsync(sale);
        }

        public async Task ConfirmSaleAsync(string saleId)
        {
            //PONTO DE ROUND-TRIP (MELHORAR DEPOIS)
            var sale = await _saleQueryRepository.GetByIdAsync(saleId);
            var response = sale.ConfirmSale();
            if (response.Value == false)
            {
                throw new StatusSaleException(response.Message ??= "STATUS INCORRETO.");
            }
            await _saleCommandRepository.ConfirmSaleAsync(sale);
        }
    }
}
