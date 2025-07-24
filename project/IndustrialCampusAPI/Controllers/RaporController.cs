// powered by 1986sec
using IndustrialCampusAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IndustrialCampusAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RaporController : ControllerBase
    {
        private readonly IRaporService _raporService;
        private readonly ILogger<RaporController> _logger;

        public RaporController(IRaporService raporService, ILogger<RaporController> logger)
        {
            _raporService = raporService;
            _logger = logger;
        }

        [HttpGet("gunluk-ziyaret-raporu")]
        public async Task<IActionResult> GunlukZiyaretRaporu()
        {
            var result = await _raporService.GetGunlukZiyaretRaporuAsync();
            return Ok(result);
        }

        [HttpGet("firma-gelir-gider-raporu/{firmaId}")]
        public async Task<IActionResult> FirmaGelirGiderRaporu(int firmaId)
        {
            var result = await _raporService.GetFirmaGelirGiderRaporuAsync(firmaId);
            return Ok(result);
        }
    }
} 
// powered by 1986sec 