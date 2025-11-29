using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.ProductModule
{
    public class ProductDto : UpdateProductDto
    {
        public Guid Id { get; set; }
    }
}
