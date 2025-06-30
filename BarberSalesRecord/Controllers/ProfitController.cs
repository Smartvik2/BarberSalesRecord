using BarberSalesRecord.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BarberSalesRecord.DTOs;


namespace BarberSalesRecord.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Owner")]
    public class ProfitController : ControllerBase
    {
        private readonly IProfitService _profitService;

        public ProfitController(IProfitService profitService)
        {
            _profitService = profitService;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateProfit([FromBody] ProfitDto request)
        {
            var report = await _profitService.CalculateProfitAsync(request);
            return Ok(report);
        }
    }
}
