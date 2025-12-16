using GearCore.Monolith.Core.TenantCore.DTOs;
using GearCore.Monolith.Core.TenantCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GearCore.Monolith.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TenantController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] ITenantQuery query)
        {
            return Ok(await query.GetAll());
        }
        
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromServices] ITenantCommand command, [FromBody] TenantRegisterDto dto)
        {
            await command.RegisterAsync(dto);
            return Ok("Registrado com sucesso");
        }
    }
}
