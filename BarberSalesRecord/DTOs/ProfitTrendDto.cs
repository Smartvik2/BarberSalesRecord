namespace BarberSalesRecord.DTOs
{
    public class ProfitTrendDto
    {
        public string Period { get; set; } = string.Empty; // e.g., "2025-06-28" or "2025-W26" or "June 2025"
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal TotalBarberProfits { get; set; }
        public decimal ShopProfit { get; set; }
    }
}
