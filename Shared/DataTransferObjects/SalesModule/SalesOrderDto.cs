namespace Shared.DataTransferObjects.SalesModule
{
    public class SalesOrderDto
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }

        public List<SalesOrderItemDto> OrderItems { get; set; } = new List<SalesOrderItemDto>();
    }
}
