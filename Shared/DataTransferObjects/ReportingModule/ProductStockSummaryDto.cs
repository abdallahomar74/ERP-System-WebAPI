using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.ReportingModule
{
    public class ProductStockSummaryDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int TotalQuantity { get; set; }
        public int WarehousesCount { get; set; }
    }
}
