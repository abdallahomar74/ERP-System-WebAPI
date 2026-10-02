namespace Shared.DataTransferObjects.SalesModule
{
    public class CreateOrderDto
    {
        public Guid CustomerId { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; } = new List<CreateOrderItemDto>();
    }
}
