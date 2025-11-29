using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.StockModule
{
    public class AdjustStockDto
    {
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public int QuantityChange { get; set; }
    }
}
