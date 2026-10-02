namespace DomainLayer.Models.SalesModule
{
    public class SalesOrder
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; } = null!;
        public Guid CustomerId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Draft;
        public decimal TotalAmount { get; set; }
        public ICollection<SalesOrderItem> SalesOrderItems { get; set; } = new List<SalesOrderItem>();

    }
    public enum OrderStatus
    {
        Draft,
        Confirmed,
        Invoiced,
        Cancelled
    }
}
