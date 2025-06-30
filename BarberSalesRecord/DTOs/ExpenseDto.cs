namespace BarberSalesRecord.DTOs
{
    public class ExpenseDto
    {
        public DateTime Date { get; set; }
        public string ExpenseName { get; set; } = null!;
        public decimal Amount { get; set; }
    }
}
