namespace BarberSalesRecord.Models
{
    public class ProfitReport
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal TotalBarberProfits { get; set; }
        public decimal ShopProfit { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
