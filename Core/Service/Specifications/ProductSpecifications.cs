using DomainLayer.Models.InventoryModule;

namespace Service.Specifications
{
    internal class ProductSpecifications : BaseSpecifications<Product>
    {
        public ProductSpecifications(List<Guid> ids) : base(p => ids.Contains(p.Id))
        {
            AddInclude(p => p.Stocks);
        }
    }
}
