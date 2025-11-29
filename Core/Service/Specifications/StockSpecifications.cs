using DomainLayer.Models.InventoryModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class StockSpecifications : BaseSpecifications<Stock>
    {
        public StockSpecifications(Guid? id) : base(s => s.Id == id)
        {
            AddInclude(s => s.Product);
            AddInclude(s => s.Warehouse);
        }
        public StockSpecifications() : base(null)
        {
            AddInclude(s => s.Product);
            AddInclude(s => s.Warehouse);
        }
    }
}
