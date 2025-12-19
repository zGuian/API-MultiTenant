using GearCore.Monolith.Core.ProductCore.DTOs;
using GearCore.Monolith.Core.ProductCore.Interfaces.Services;
using GearCore.Monolith.WebApi.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace GearCore.Monolith.WebApi.Controllers
{
    [ApiController]
    [RequireTenant]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IProductQuery query,
            CancellationToken ct, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 15)
        {

            var result = await query.GetAllAsync(pageIndex, pageSize, ct);
            var totalItems = await query.CountAsync(ct);
            return Ok(new
            {
                pagination = new
                {
                    currentPage = pageIndex,
                    pageSize = pageSize,
                    totalItems
                },
                items = result
            });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromServices] IProductQuery productQuery
            , [FromRoute] string id
            , CancellationToken ct = default)
        {
            var result = await productQuery.GetByIdAsync(id, ct);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromServices] IProductCommand productCommand
            , [FromBody] ProductRegisterDto dto
            , CancellationToken cancellationToken)
        {
            var result = await productCommand.RegisterAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromServices] IProductCommand productCommand
            , [FromBody] ProductUpdateDto dto
            , CancellationToken cancellationToken)
        {
            await productCommand.UpdateAsync(dto, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromServices] IProductCommand productCommand
            , [FromRoute] string id
            , CancellationToken cancellationToken)
        {
            await productCommand.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
