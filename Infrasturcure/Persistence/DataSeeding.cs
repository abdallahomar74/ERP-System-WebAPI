using DomainLayer.Contracts;
using DomainLayer.Models.InventoryModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(ErpDbContext _dbContext) : IDataSeeding
    {
        public async Task DataSeedingAsync()
        {
            try
            {
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                if (!_dbContext.Warehouses.Any())
                {
                    var warehousesData = File.OpenRead(@"..\Infrasturcure\Persistence\Data\DataSeed\warehouses.json");
                    var warehouses = await JsonSerializer.DeserializeAsync<List<Warehouse>>(warehousesData);
                    if (warehouses != null && warehouses.Any())
                        await _dbContext.Warehouses.AddRangeAsync(warehouses);
                }

                if (!_dbContext.Products.Any())
                {
                    var productsData = File.OpenRead(@"..\Infrasturcure\Persistence\Data\DataSeed\products.json");
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productsData);
                    if (products != null && products.Any())
                        await _dbContext.Products.AddRangeAsync(products);
                }

                if (!_dbContext.Stocks.Any())
                {
                    var stocksData = File.OpenRead(@"..\Infrasturcure\Persistence\Data\DataSeed\stocks.json");
                    var stocks = await JsonSerializer.DeserializeAsync<List<Stock>>(stocksData);
                    if (stocks != null && stocks.Any())
                        await _dbContext.Stocks.AddRangeAsync(stocks);
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
                // TODO: Log Exception
            }
        }
    }
}
