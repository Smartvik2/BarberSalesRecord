namespace BarberSalesRecord.DTOs
{
    public class ServiceRecordDto
    {
        public int BarberId { get; set; }
        public string BarberName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string ServiceName { get; set; } = null!;
        public decimal AmountCharged { get; set; }
    }
}
