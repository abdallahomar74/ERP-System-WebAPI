using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.WarehouseModule
{
    public class CreateWarehouseDto
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Location { get; set; }
    }
}
