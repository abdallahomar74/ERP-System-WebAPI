using DomainLayer.Models.InventoryModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks").HasKey(s => s.Id);
            builder.Property(s => s.Quantity).IsRequired().HasDefaultValue(0);
            builder.Property(s => s.LastUpdated).HasDefaultValueSql("GETUTCDATE()");
            builder.HasIndex(s => new { s.ProductId, s.WarehouseId }).IsUnique();

        }
    }
}
