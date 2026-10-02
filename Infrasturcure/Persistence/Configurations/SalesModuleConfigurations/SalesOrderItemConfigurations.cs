using DomainLayer.Models.InventoryModule;
using DomainLayer.Models.SalesModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.SalesModuleConfigurations
{
    public class SalesOrderItemConfigurations : IEntityTypeConfiguration<SalesOrderItem>
    {
        public void Configure(EntityTypeBuilder<SalesOrderItem> builder)
        {
            builder.ToTable("SalesOrderItems").HasKey(i => i.Id);
            builder.Property(s => s.Quantity).IsRequired().HasDefaultValue(0);
            builder.Property(soi => soi.LineTotal).HasColumnType("decimal(18,2)");
            builder.Property(soi => soi.UnitPrice).HasColumnType("decimal(18,2)");
            builder.HasOne<SalesOrder>()
                 .WithMany()
                 .HasForeignKey(soi => soi.SalesOrderId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(soi => soi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(soi => new { soi.SalesOrderId, soi.ProductId })
                .IsUnique();
        }
    }
}
