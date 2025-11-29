using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.ProductModule
{
    public class CreateProductDto
    {
        public string SKU { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public string Unit { get; set; } = "pcs";
        public int ReorderLevel { get; set; } = 5;
    }
}
