using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(typeof(Service.AssemblyReference).Assembly);
            Services.AddScoped<IServiceManger, ServiceManagerWithFactoryDelegate>();

           
            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<Func<IProductService>>(
               Provider => () => Provider.GetRequiredService<IProductService>());
 
            Services.AddScoped<IWarehouseService, WarehouseService>();
            Services.AddScoped<Func<IWarehouseService>>(
               Provider => () => Provider.GetRequiredService<IWarehouseService>());
 
            Services.AddScoped<IStockService, StockService>();
            Services.AddScoped<Func<IStockService>>(
               Provider => () => Provider.GetRequiredService<IStockService>());

            Services.AddScoped<IInventoryReportService, InventoryReportService>();
            Services.AddScoped<Func<IInventoryReportService>>(
               Provider => () => Provider.GetRequiredService<IInventoryReportService>());
            return Services;
        }
    }
}
