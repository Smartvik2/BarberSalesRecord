﻿namespace BarberSalesRecord.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string ExpenseName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        //public string? Category { get; set; }
        public DateTime Date { get; set; }
    }
}
