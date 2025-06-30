using BarberSalesRecord.DTOs;
using BarberSalesRecord.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BarberSalesRecord.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddExpense([FromBody] ExpenseDto dto)
        {
            await _expenseService.CreateAsync(dto);
            return Ok("Expense added successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            var expenses = await _expenseService.GetAllAsync();
            return Ok(expenses);
        }

        [HttpGet("by-date")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var result = await _expenseService.GetByDateRangeAsync(start, end);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var result = await _expenseService.DeleteExpenseAsync(id);
            //return result ? NoContent() : NotFound("Expense not found");
            return Ok(new { message = "Expense removed successfully." });
        }
    }
}
