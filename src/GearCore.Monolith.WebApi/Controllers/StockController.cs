using GearCore.Monolith.Core.StockCore.DTOs;
using GearCore.Monolith.Core.StockCore.Interfaces.Services;
using GearCore.Monolith.Core.TenantCore.Entities.Enums;
using GearCore.Monolith.WebApi.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GearCore.Monolith.WebApi.Controllers
{
    [Authorize(Roles = nameof(RoleTenantEnum.SYSTEM_USER))]
    [RequireTenant]
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
