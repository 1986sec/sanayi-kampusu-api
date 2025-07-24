// powered by 1986sec
using IndustrialCampusAPI.DTOs;
using IndustrialCampusAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IndustrialCampusAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FirmaController : ControllerBase
    {
        private readonly IFirmaService _firmaService;
        private readonly ILogger<FirmaController> _logger;

        public FirmaController(IFirmaService firmaService, ILogger<FirmaController> logger)
        {
            _firmaService = firmaService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FirmaDTO>>> GetAll()
        {
            var result = await _firmaService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FirmaDTO>> GetById(int id)
        {
            var result = await _firmaService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<FirmaDTO>> Create([FromBody] FirmaCreateDTO dto)
        {
            var result = await _firmaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.FirmaID }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FirmaDTO>> Update(int id, [FromBody] FirmaUpdateDTO dto)
        {
            var result = await _firmaService.UpdateAsync(id, dto);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _firmaService.DeleteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
} 
// powered by 1986sec 