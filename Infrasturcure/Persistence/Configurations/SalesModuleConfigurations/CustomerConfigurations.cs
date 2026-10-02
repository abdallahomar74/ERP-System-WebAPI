using DomainLayer.Models.SalesModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.SalesModuleConfigurations
{
    public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers").HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Email).HasMaxLength(50);
            builder.Property(p => p.Phone).HasMaxLength(50);
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(p => p.IsActive).HasDefaultValue(true);

        }
    }
}
