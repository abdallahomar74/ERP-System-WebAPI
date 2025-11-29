using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.WarehouseModule
{
    public class WarehouseDto : UpdateWarehouseDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
