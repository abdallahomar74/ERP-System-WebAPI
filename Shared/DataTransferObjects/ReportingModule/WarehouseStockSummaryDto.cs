using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.ReportingModule
{
    public class WarehouseStockSummaryDto
    {
        public Guid WarehouseId { get; set; }
        public string WarehouseName { get; set; } = null!;
        public int TotalQuantity { get; set; }
        public int DistinctProductsCount { get; set; }
    }
}
