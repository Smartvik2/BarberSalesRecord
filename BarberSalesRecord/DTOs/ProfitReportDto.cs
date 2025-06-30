namespace BarberSalesRecord.DTOs
{
    public class ProfitReportDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ShopName { get; set; } = string.Empty;
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal TotalBarberProfits { get; set; }
        public decimal ShopProfit { get; set; }
        public List<BarberProfitDto> BarberProfits { get; set; }
    }
}
