using Shared.DataTransferObjects.ReportingModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IInventoryReportService
    {
        Task<IEnumerable<LowStockProductDto>> GetLowStockProductsAsync();
        Task<IEnumerable<ProductStockSummaryDto>> GetProductStockSummaryAsync();
        Task<IEnumerable<WarehouseStockSummaryDto>> GetWarehouseStockSummaryAsync();
        Task<IEnumerable<ProductWarehouseStockDto>> GetProductStockDetailsAsync(Guid productId);
    }
}
