namespace DomainLayer.Models.SalesModule
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public Guid SalesOrderId { get; set; }
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public bool IsPaid { get; set; } = false;
        public decimal TotalAmount { get; set; }

    }
}
