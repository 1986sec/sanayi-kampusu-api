// powered by 1986sec
using IndustrialCampusAPI.DTOs;
using IndustrialCampusAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IndustrialCampusAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZiyaretController : ControllerBase
    {
        private readonly IZiyaretService _service;
        private readonly ILogger<ZiyaretController> _logger;

        public ZiyaretController(IZiyaretService service, ILogger<ZiyaretController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZiyaretDTO>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ZiyaretDTO>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ZiyaretDTO>> Create([FromBody] ZiyaretCreateDTO dto)
        {
            // TODO: Gerçek projede kullanıcı kimliği JWT'den alınmalı
            int kullaniciId = 1;
            var result = await _service.CreateAsync(dto, kullaniciId);
            return CreatedAtAction(nameof(GetById), new { id = result.ZiyaretID }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ZiyaretDTO>> Update(int id, [FromBody] ZiyaretUpdateDTO dto)
        {
            // TODO: Gerçek projede kullanıcı kimliği ve rolü JWT'den alınmalı
            int kullaniciId = 1;
            string rol = "Admin";
            var result = await _service.UpdateAsync(id, dto, kullaniciId, rol);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // TODO: Gerçek projede kullanıcı kimliği ve rolü JWT'den alınmalı
            int kullaniciId = 1;
            string rol = "Admin";
            var success = await _service.DeleteAsync(id, kullaniciId, rol);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
// powered by 1986sec 