using DomainLayer.Models.SalesModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.SalesModuleConfigurations
{
    public class SalesOrderConfigurations : IEntityTypeConfiguration<SalesOrder>
    {
        public void Configure(EntityTypeBuilder<SalesOrder> builder)
        {
            builder.ToTable("SalesOrders").HasKey(i => i.Id);
            builder.Property(so => so.OrderNumber).IsRequired().HasMaxLength(50);
            builder.HasIndex(so => so.OrderNumber).IsUnique();
            builder.Property(so => so.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(so => so.Status).HasDefaultValue(OrderStatus.Draft);
            builder.Property(so => so.TotalAmount).HasColumnType("decimal(18,2)");
            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(so => so.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
