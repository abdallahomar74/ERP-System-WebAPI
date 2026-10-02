using DomainLayer.Models.SalesModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.SalesModuleConfigurations
{
    public class InvoiceConfigurations : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices").HasKey(i => i.Id);
            builder.Property(i => i.InvoiceNumber).IsRequired().HasMaxLength(50);
            builder.HasIndex(i => i.InvoiceNumber).IsUnique();
            builder.Property(i => i.IssuedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(i => i.IsPaid).HasDefaultValue(false);
            builder.Property(i => i.TotalAmount).HasColumnType("decimal(18,2)");
            builder.HasOne<SalesOrder>()
                .WithOne()
                .HasForeignKey<Invoice>(i => i.SalesOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(i => i.SalesOrderId).IsUnique();
        }
    }
}
