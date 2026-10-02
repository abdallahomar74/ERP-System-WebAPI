namespace DomainLayer.Models.SalesModule
{
    public class SalesOrderItem
    {
        public Guid Id { get; set; }
        public Guid SalesOrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public SalesOrder SalesOrder { get; set; } = null!;

    }
}
