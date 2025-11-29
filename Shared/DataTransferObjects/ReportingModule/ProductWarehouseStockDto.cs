using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.ReportingModule
{
    public class ProductWarehouseStockDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public Guid WarehouseId { get; set; }
        public string WarehouseName { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
