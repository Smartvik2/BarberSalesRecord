using BarberSalesRecord.DTOs;
using BarberSalesRecord.Models;

namespace BarberSalesRecord.Interfaces
{
    public interface IExpenseService
    {
        Task<Expense> CreateAsync(ExpenseDto dto);
        Task<List<Expense>> GetAllAsync();
        Task<List<Expense>> GetByDateRangeAsync(DateTime start, DateTime end);
        Task<bool> DeleteExpenseAsync(int id);

    }
}
