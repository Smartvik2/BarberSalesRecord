namespace BarberSalesRecord.Models
{
    public class Barber
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ServiceRecord> ServiceRecords { get; set; }
    }
}
