using BarberSalesRecord.DTOs;
using BarberSalesRecord.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace BarberSalesRecord.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BarbersController : ControllerBase
    {
        private readonly IBarberService _barberService;

        public BarbersController(IBarberService barberService)
        {
            _barberService = barberService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBarber([FromBody] BarbersDto dto)
        {
            await _barberService.AddBarberAsync(dto);
            return Ok("Barber added successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetBarbers()
        {
            var barbers = await _barberService.GetBarbersAsync();
            return Ok(barbers);
        }


    }
}
