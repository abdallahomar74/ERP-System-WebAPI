using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.InventoryModule
{
    public class Product
    {
        public Guid Id { get; set; }
        public string SKU { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public string Unit { get; set; } = "pcs";
        public int ReorderLevel { get; set; } = 5;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public ICollection<Stock>? Stocks { get; set; }
        //public ICollection<OrderItem>? OrderItems { get; set; }
        //public ICollection<PurchaseItem>? PurchaseItems { get; set; }
    }
}
