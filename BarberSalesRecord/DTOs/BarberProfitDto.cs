namespace BarberSalesRecord.DTOs
{
    public class BarberProfitDto
    {
        public int BarberId { get; set; }
        public string BarberName { get; set; } = string.Empty;
        public decimal TotalIncome { get; set; }
        public decimal Profit { get; set; }
    }
}
