using GearCore.Monolith.Core.SalesCore.DTOs.Requests;
using GearCore.Monolith.Core.SalesCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GearCore.Monolith.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SaleController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromServices] ISalesQuery query,
            CancellationToken ct, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 15)
        {
            var sales = await query.GetAllAsync(pageIndex, pageSize, ct);
            var countSales = await query.CountAsync(ct);
            return Ok(new
            {
                currentPage = pageIndex,
                pageSize,
                countSales,
                items = sales
            });
        }

        [HttpPost("RealizeOrder")]
        public async Task<IActionResult> OrderAsync([FromServices] ISalesCommand command
            , [FromBody] RealizeSaleRequest request)
        {
            await command.ExecuteOrderAsync(request);
            return Created();
        }

        [HttpPut("ConfirmSale/{saleId}")]
        public async Task<IActionResult> ConfirmSale([FromServices] ISalesCommand command, string saleId)
        {
            await command.ConfirmSaleAsync(saleId);
            return NoContent();
        }
    }
}
