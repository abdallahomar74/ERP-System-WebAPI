using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.ReportingModule;

namespace Presentation.Controllers
{
    public class InventoryReportsController(IServiceManger _serviceManger) : ApiBaseController
    {
        [HttpGet("low-stock-products")]
        public async Task<ActionResult<IEnumerable<LowStockProductDto>>> GetLowStockProductsAsync()
        {
            var lowStockProducts = await _serviceManger.InventoryReportService.GetLowStockProductsAsync();
            return Ok(lowStockProducts);
        }
        [HttpGet("product-stock-summary")]
        public async Task<ActionResult<IEnumerable<ProductStockSummaryDto>>> GetProductStockSummaryAsync()
        {
            var productStockSummary = await _serviceManger.InventoryReportService.GetProductStockSummaryAsync();
            return Ok(productStockSummary);
        }
        [HttpGet("warehouse-stock-summary")]
        public async Task<ActionResult<IEnumerable<WarehouseStockSummaryDto>>> GetWarehouseStockSummaryAsync()
        {
            var warehouseStockSummary = await _serviceManger.InventoryReportService.GetWarehouseStockSummaryAsync();
            return Ok(warehouseStockSummary);
        }
        [HttpGet("product-stock-details/{productId:guid}")]
        public async Task<ActionResult<IEnumerable<ProductWarehouseStockDto>>> GetProductStockDetailsAsync(Guid productId)
        {
            var productStockDetails = await _serviceManger.InventoryReportService.GetProductStockDetailsAsync(productId);
            if (productStockDetails == null || !productStockDetails.Any())
            {
                return NotFound($"No stock details found for product with ID: {productId}");
            }
            return Ok(productStockDetails);
        }

    }
}
