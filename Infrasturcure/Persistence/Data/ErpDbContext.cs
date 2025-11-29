using DomainLayer.Models.InventoryModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class ErpDbContext: DbContext
    {
        public ErpDbContext(DbContextOptions<ErpDbContext> options): base(options)
        {
        }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Product> Products  { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
        }
    }
}
