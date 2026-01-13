using GearCore.Monolith.Core.SalesCore.DTOs.Requests;

namespace GearCore.Monolith.Core.SalesCore.Interfaces.Services
{
    public interface ISalesCommand
    {
        Task ExecuteOrderAsync(RealizeSaleRequest request);
        Task ConfirmSaleAsync(string request);
    }
}
