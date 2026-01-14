using GearCore.Monolith.Core.Exceptions;
using GearCore.Monolith.Core.SalesCore.DTOs.Requests;
using GearCore.Monolith.Core.SalesCore.Entities;
using GearCore.Monolith.Core.SalesCore.Interfaces.Repositories;
using GearCore.Monolith.Core.SalesCore.Interfaces.Services;
using GearCore.Monolith.Core.StockCore.Interfaces.Repositories;

namespace GearCore.Monolith.Core.SalesCore.Services
{
    public class SalesCommand(ISalesCommandRepository saleCommandRepository
        , ISalesQueryRepository saleQueryRepository
        , IStockQueryRepository stockQueryRepository) : ISalesCommand
    {
        private readonly ISalesCommandRepository _saleCommandRepository = saleCommandRepository;
        private readonly ISalesQueryRepository _saleQueryRepository = saleQueryRepository;
        private readonly IStockQueryRepository _stockQueryRepository = stockQueryRepository;

        public async Task ExecuteOrderAsync(RealizeSaleRequest request)
        {
            //PONTO DE ROUND-TRIP (MELHORAR DEPOIS)
            var stocks = await _stockQueryRepository.GetManyProductInStock(request.Items.Select(rs => rs.ProductId));

            var saleItems = new List<SaleItem>();
            foreach (var item in request.Items)
            {
                var stock = stocks.SingleOrDefault(s => s.Product.Id == item.ProductId);
                if (stock == null)
                {
                    continue;
                }

                if (stock.Quantity >= item.Quantity)
                {
                    stock.MakeReservation(item.Quantity);
                    saleItems.Add(new SaleItem(stock.Product, item.Quantity));
                }
            }

            var sale = new Sales(saleItems, request.UserId);
            await _saleCommandRepository.ExecuteSaleAsync(sale, stocks);
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
