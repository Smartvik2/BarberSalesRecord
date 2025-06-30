namespace BarberSalesRecord.Models
{
    public class ServiceRecord
    {
        public int Id { get; set; }
        public int BarberId { get; set; }
        public string ServiceType { get; set; } = string.Empty;
        public decimal AmountCharged { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }

        public Barber? Barber { get; set; }
    }
}
