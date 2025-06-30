namespace BarberSalesRecord.DTOs
{
    public class ProfitDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Dictionary<int, decimal> BarberPercentages { get; set; }
    }
}
