using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheTrail.Services.Core.DTOs.Era;
using TheTrail.Services.Core.Interfaces;

namespace TheTrail.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ErasController : ControllerBase
    {
        private readonly IEraService _eraService;

        public ErasController(IEraService eraService)
        {
            _eraService = eraService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EraDto>>> GetAll()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            IEnumerable<EraDto> eras = await _eraService.GetAllAsync(userId);

            return Ok(eras);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EraDto>> GetById(int id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            EraDto? era = await _eraService.GetByIdAsync(id, userId);

            if (era == null)
            {
                return NotFound(new { message = $"Era with id {id} was not found." });
            }

            return Ok(era);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<EraDto>> Create([FromBody] CreateEraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EraDto created = await _eraService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<EraDto>> Update(int id, [FromBody] UpdateEraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EraDto? updated = await _eraService.UpdateAsync(id, dto);

            if (updated == null)
            {
                return NotFound(new { message = $"Era with id {id} was not found." });
            }

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _eraService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new { message = $"Era with id {id} was not found." });
            }

            return NoContent();
        }
    }
}