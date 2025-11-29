using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.WarehouseModule
{
    public class UpdateWarehouseDto : CreateWarehouseDto
    {
        public bool IsActive { get; set; } = true;
    }
}
