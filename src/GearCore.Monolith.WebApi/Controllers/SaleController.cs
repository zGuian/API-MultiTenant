using GearCore.Monolith.Core.SalesCore.DTOs;
using GearCore.Monolith.Core.SalesCore.DTOs.Requests;
using GearCore.Monolith.Core.SalesCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GearCore.Monolith.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SaleController : ControllerBase
    {
        [HttpPost("RealizeSale")]
        public async Task<IActionResult> RealizeSale([FromServices] ISalesCommand command
            , [FromBody] RealizeSaleRequest request)
        {
            await command.ExecuteSaleAsync(request);
            return Ok("Produtos reservados e aguardando confirmação.");
        }


        [HttpPost("ConfirmSale")]
        public async Task<IActionResult> ConfirmSale([FromServices] ISalesCommand command)
        {


            return BadRequest();
        }
    }
}
