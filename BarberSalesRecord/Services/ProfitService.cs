using BarberSalesRecord.Data;
using BarberSalesRecord.DTOs;
using BarberSalesRecord.Interfaces;
using BarberSalesRecord.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BarberSalesRecord.Services
{
    public class ProfitService : IProfitService
    {
        private readonly ApplicationDbContext _context;

        public ProfitService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProfitReportDto> CalculateProfitAsync(ProfitDto request)
        {
            var incomeRecords = await _context.ServiceRecords
                .Include(r => r.Barber)
                .Where(r => r.Date >= request.StartDate && r.Date <= request.EndDate)
                .ToListAsync();

            var expenseRecords = await _context.Expenses
                .Where(e => e.Date >= request.StartDate && e.Date <= request.EndDate)
                .ToListAsync();

            decimal totalIncome = incomeRecords.Sum(r => r.AmountCharged);
            decimal totalExpenses = expenseRecords.Sum(e => e.Amount);

            var barberGroups = incomeRecords
                .GroupBy(r => r.BarberId)
                .Select(group =>
                {
                    var barber = group.First().Barber;
                    var total = group.Sum(g => g.AmountCharged);
                    var percent = request.BarberPercentages.TryGetValue(barber.Id, out var p) ? p : 0;
                    var profit = (percent / 100m) * total;

                    return new BarberProfitDto
                    {
                        BarberId = barber.Id,
                        BarberName = barber.Name,
                        TotalIncome = total,
                        Profit = profit
                    };
                }).ToList();

            decimal totalBarberProfits = barberGroups.Sum(b => b.Profit);
            decimal shopProfit = totalIncome - totalBarberProfits - totalExpenses;

            return new ProfitReportDto
            {
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                TotalBarberProfits = totalBarberProfits,
                ShopProfit = shopProfit,
                BarberProfits = barberGroups
            };
        }

            public async Task<List<ProfitTrendDto>> GetProfitTrendsAsync(DateTime start, DateTime end, string groupBy)
            {
            var records = await _context.ServiceRecords
                .Include(r => r.Barber)
                .Where(r => r.Date >= start && r.Date <= end)
                .ToListAsync();

            var expenses = await _context.Expenses
                .Where(e => e.Date >= start && e.Date <= end)
                .ToListAsync();

            var groups = records
                .GroupBy(r =>
                {
                    return groupBy.ToLower() switch
                    {
                        "weekly" => $"{CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(r.Date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)}-{r.Date.Year}",
                        "monthly" => r.Date.ToString("MMMM yyyy"),
                        _ => r.Date.ToString("yyyy-MM-dd")
                    };
                });

            var trends = new List<ProfitTrendDto>();

            foreach (var group in groups)
            {
                var periodRecords = group.ToList();
                var periodStart = periodRecords.Min(r => r.Date);
                var periodEnd = periodRecords.Max(r => r.Date);

                var periodExpenses = expenses.Where(e => e.Date >= periodStart && e.Date <= periodEnd).ToList();

                decimal income = periodRecords.Sum(r => r.AmountCharged);
                decimal expense = periodExpenses.Sum(e => e.Amount);

                decimal totalBarberProfit = 0;
                foreach (var r in periodRecords)
                {
                    decimal percentage = 40; // default or dynamic if needed
                    totalBarberProfit += (percentage / 100m) * r.AmountCharged;
                }

                trends.Add(new ProfitTrendDto
                {
                    Period = group.Key,
                    TotalIncome = income,
                    TotalExpenses = expense,
                    TotalBarberProfits = totalBarberProfit,
                    ShopProfit = income - expense - totalBarberProfit
                });
            }

            return trends.OrderBy(t => t.Period).ToList();
            }

        public async Task SaveProfitReportAsync(ProfitReportDto reportDto, DateTime start, DateTime end)
        {
            var report = new ProfitReport
            {
                StartDate = start,
                EndDate = end,
                TotalIncome = reportDto.TotalIncome,
                TotalExpenses = reportDto.TotalExpenses,
                TotalBarberProfits = reportDto.TotalBarberProfits,
                ShopProfit = reportDto.ShopProfit
            };

            _context.ProfitReports.Add(report);
            await _context.SaveChangesAsync();
        }



    }
}

