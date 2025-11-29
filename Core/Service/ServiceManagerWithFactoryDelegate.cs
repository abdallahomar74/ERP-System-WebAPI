using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManagerWithFactoryDelegate(Func<IProductService> ProductFactory,
        Func<IWarehouseService> WarehouseFactory,
        Func<IStockService> StockFactory,
        Func<IInventoryReportService> InventoryReportFactory) : IServiceManger
    {
        public IProductService ProductService => ProductFactory.Invoke();

        public IWarehouseService WarehouseService => WarehouseFactory.Invoke();

        public IStockService StockService => StockFactory.Invoke();

        public IInventoryReportService InventoryReportService => InventoryReportFactory.Invoke();
    }
}
