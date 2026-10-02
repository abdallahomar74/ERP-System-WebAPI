using Shared.DataTransferObjects.SalesModule;

namespace ServiceAbstraction
{
    public interface ISalesService
    {
        Task<SalesOrderDto> CreateSalesOrderAsync(CreateOrderDto createSalesOrderDto);
        Task<SalesOrderDto> GetSalesOrderByIdAsync(Guid orderId);
    }
}
