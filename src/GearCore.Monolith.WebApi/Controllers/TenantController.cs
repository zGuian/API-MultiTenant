using GearCore.Monolith.Core.TenantCore.DTOs;
using GearCore.Monolith.Core.TenantCore.Interfaces.Services;
using GearCore.Monolith.WebApi.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace GearCore.Monolith.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TenantController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] ITenantQuery query, CancellationToken ct)
        {
            return Ok(await query.GetAllAsync(ct));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] ITenantQuery query, [FromRoute] string id, CancellationToken ct)
        {
            var dto = await query.GetByIdAsync(id, ct);
            return Ok(dto);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromServices] ITenantCommand command, [FromBody] TenantRegisterDto dto)
        {
            await command.RegisterAsync(dto);
            return Ok("Registrado com sucesso");
        }

        [RequireTenant]
        [HttpPost("LinkUser/{userID}")]
        public async Task<IActionResult> LinkUser([FromServices] ITenantCommand command, [FromRoute] string userID,
            CancellationToken ct)
        {
            await command.LinkToUserAsync(userID, ct);
            return Ok();
        }
    }
}
