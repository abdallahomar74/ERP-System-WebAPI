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
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.ToTable("Warehouses").HasKey(w => w.Id);
            builder.Property(w => w.Name).IsRequired().HasMaxLength(150);
            builder.Property(w => w.Code).IsRequired().HasMaxLength(50);
            builder.HasIndex(w => w.Code).IsUnique();
            builder.Property(w => w.Location).HasMaxLength(200);
            builder.Property(w => w.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(w => w.IsActive).HasDefaultValue(true);
            builder.HasMany(p => p.Stocks)
                   .WithOne(s => s.Warehouse)
                  .HasForeignKey(s => s.WarehouseId)
                  .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
