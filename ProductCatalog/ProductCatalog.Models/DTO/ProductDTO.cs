using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Models.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
