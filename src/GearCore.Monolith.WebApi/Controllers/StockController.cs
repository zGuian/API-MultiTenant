using GearCore.Monolith.Core.StockCore.DTOs;
using GearCore.Monolith.Core.StockCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GearCore.Monolith.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/Stocks")]
    public class StockController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllStocks([FromServices] IStockQuery query
            , CancellationToken ct)
        {
            var stocks = await query.GetAllStocksAsync(ct);
            return Ok(stocks);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetStockByProductId([FromServices] IStockQuery query
            , [FromRoute] Guid id
            , CancellationToken ct)
        {
            var stock =  await query.GetByIdAsync(id, ct);
            return Ok(stock);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterStock([FromServices] IStockCommand command
            , [FromBody] RegisterStockDto dto
            , CancellationToken ct)
        {
            await command.AddStockAsync(dto, ct);
            return Ok("Stock register for success");
        }
    }
}
