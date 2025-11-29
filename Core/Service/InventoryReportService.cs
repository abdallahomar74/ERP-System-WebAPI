using DomainLayer.Contracts;
using DomainLayer.Models.InventoryModule;
using ServiceAbstraction;
using Shared.DataTransferObjects.ReportingModule;

namespace Service
{
    public class InventoryReportService(IUnitOfWork _unitOfWork) : IInventoryReportService
    {
        // retun the list of products where stock quantity is below reorder level
        public async Task<IEnumerable<LowStockProductDto>> GetLowStockProductsAsync()
        {
            var stockRepo = _unitOfWork.GetRepository<Stock>();
            var productRepo = _unitOfWork.GetRepository<Product>();

            var allStocks = await stockRepo.GetAllAsync();
            var allProducts = await productRepo.GetAllAsync();

            var stockByProduct = allStocks
                .GroupBy(s => s.ProductId)
                .ToDictionary(
                    g => g.Key,
                   g => g.Sum(s => s.Quantity));

            var lowStockProducts = allProducts.Select(p =>
            {
                stockByProduct.TryGetValue(p.Id, out int totalQuantity);
                return new LowStockProductDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    TotalQuantity = totalQuantity,
                    ReorderLevel = p.ReorderLevel
                };
            }).Where(dto => dto.TotalQuantity < dto.ReorderLevel)
            .ToList();
            return lowStockProducts;
        }

        public async Task<IEnumerable<ProductStockSummaryDto>> GetProductStockSummaryAsync()
        {
            var productRepo = _unitOfWork.GetRepository<Product>();
            var stockRepo = _unitOfWork.GetRepository<Stock>();

            var products = await productRepo.GetAllAsync();
            var stocks = await stockRepo.GetAllAsync();

            var stockGroups = stocks
                .GroupBy(s => s.ProductId)
                .ToDictionary(
                    g => g.Key,
                    g => new
                    {
                        TotalQuantity = g.Sum(s => s.Quantity),
                        WarehousesCount = g
                            .Select(s => s.WarehouseId)
                            .Distinct()
                            .Count()
                    });

            var result = products
                .Select(p =>
                {
                    if (!stockGroups.TryGetValue(p.Id, out var info))
                    {

                        return new ProductStockSummaryDto
                        {
                            ProductId = p.Id,
                            ProductName = p.Name,
                            TotalQuantity = 0,
                            WarehousesCount = 0
                        };
                    }

                    return new ProductStockSummaryDto
                    {
                        ProductId = p.Id,
                        ProductName = p.Name,
                        TotalQuantity = info.TotalQuantity,
                        WarehousesCount = info.WarehousesCount
                    };
                })
                .OrderBy(r => r.ProductName)
                .ToList();

            return result;
        }


        public async Task<IEnumerable<WarehouseStockSummaryDto>> GetWarehouseStockSummaryAsync()
        {
            var warehouseRepo = _unitOfWork.GetRepository<Warehouse>();
            var stockRepo = _unitOfWork.GetRepository<Stock>();

            var warehouses = await warehouseRepo.GetAllAsync();
            var stocks = await stockRepo.GetAllAsync();


            var stockGroups = stocks
                .GroupBy(s => s.WarehouseId)
                .ToDictionary(
                    g => g.Key,
                    g => new
                    {
                        TotalQuantity = g.Sum(s => s.Quantity),
                        DistinctProductsCount = g
                            .Select(s => s.ProductId)
                            .Distinct()
                            .Count()
                    });

            var result = warehouses
                .Select(w =>
                {
                    if (!stockGroups.TryGetValue(w.Id, out var info))
                    {
                        return new WarehouseStockSummaryDto
                        {
                            WarehouseId = w.Id,
                            WarehouseName = w.Name,
                            TotalQuantity = 0,
                            DistinctProductsCount = 0
                        };
                    }

                    return new WarehouseStockSummaryDto
                    {
                        WarehouseId = w.Id,
                        WarehouseName = w.Name,
                        TotalQuantity = info.TotalQuantity,
                        DistinctProductsCount = info.DistinctProductsCount
                    };
                })
                .OrderBy(r => r.WarehouseName)
                .ToList();

            return result;
        }

        public async Task<IEnumerable<ProductWarehouseStockDto>> GetProductStockDetailsAsync(Guid productId)
        {
            var productRepo = _unitOfWork.GetRepository<Product>();
            var warehouseRepo = _unitOfWork.GetRepository<Warehouse>();
            var stockRepo = _unitOfWork.GetRepository<Stock>();

            var products = await productRepo.GetAllAsync();
            var warehouses = await warehouseRepo.GetAllAsync();
            var stocks = await stockRepo.GetAllAsync();

            var product = products.FirstOrDefault(p => p.Id == productId);
            if (product is null)
            {
                return Enumerable.Empty<ProductWarehouseStockDto>();
            }

            var productStocks = stocks
                .Where(s => s.ProductId == productId)
                .ToList();

            var result = productStocks
                .Join(
                    warehouses,
                    s => s.WarehouseId,
                    w => w.Id,
                    (s, w) => new ProductWarehouseStockDto
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        WarehouseId = w.Id,
                        WarehouseName = w.Name,
                        Quantity = s.Quantity
                    })
                .OrderBy(r => r.WarehouseName)
                .ToList();

            return result;
        }
    }
}
