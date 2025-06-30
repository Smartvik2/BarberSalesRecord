using BarberSalesRecord.DTOs;
using BarberSalesRecord.Interfaces;
using BarberSalesRecord.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberSalesRecord.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServiceRecordsController : ControllerBase
    {
        private readonly IServiceRecordService _recordService;

        public ServiceRecordsController(IServiceRecordService recordService)
        {
            _recordService = recordService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddServiceRecord([FromBody] ServiceRecordDto dto)
        {
            await _recordService.CreateAsync(dto);
            return Ok("Service record added.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecords()
        {
            var records = await _recordService.GetAllAsync();
            return Ok(records);
        }

        [HttpGet("by-barber/{barberId:int}")]
        public async Task<IActionResult> GetByBarber(int barberId)
        {
            var result = await _recordService.GetByBarberAsync(barberId);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceRecord(int id)
        {
            var result = await _recordService.DeleteServiceRecordAsync(id);
            //return result ? NoContent() : NotFound("Service record not found");
            return Ok(new { message = "Service record removed successfully." });
        }
    }
}
