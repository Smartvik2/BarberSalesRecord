using BarberSalesRecord.Data;
using BarberSalesRecord.Interfaces;
using BarberSalesRecord.Models;
using BarberSalesRecord.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BarberSalesRecord.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ApplicationDbContext _context;

        public ExpenseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Expense> CreateAsync(ExpenseDto dto)
        {
            var e = new Expense
            {
                Date = dto.Date,
                ExpenseName = dto.ExpenseName,
                Amount = dto.Amount
            };
            _context.Expenses.Add(e);
            await _context.SaveChangesAsync();
            return e;
        }

        public Task<List<Expense>> GetAllAsync() =>
        _context.Expenses.ToListAsync();

        public Task<List<Expense>> GetByDateRangeAsync(DateTime start, DateTime end) =>
            _context.Expenses
                .Where(e => e.Date >= start && e.Date <= end)
                .ToListAsync();

        public async Task<bool> DeleteExpenseAsync(int id)
        {
            var exp = await _context.Expenses.FindAsync(id);
            if (exp == null) return false;

            _context.Expenses.Remove(exp);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
