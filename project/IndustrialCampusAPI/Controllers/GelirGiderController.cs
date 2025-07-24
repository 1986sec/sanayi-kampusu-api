using IndustrialCampusAPI.DTOs;
using IndustrialCampusAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IndustrialCampusAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GelirGiderController : ControllerBase
    {
        private readonly IGelirGiderService _service;
        private readonly ILogger<GelirGiderController> _logger;

        public GelirGiderController(IGelirGiderService service, ILogger<GelirGiderController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GelirGiderDTO>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GelirGiderDTO>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<GelirGiderDTO>> Create([FromBody] GelirGiderCreateDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.GelirGiderID }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GelirGiderDTO>> Update(int id, [FromBody] GelirGiderUpdateDTO dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
} 