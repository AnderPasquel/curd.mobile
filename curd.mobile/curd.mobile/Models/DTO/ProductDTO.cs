using System;
using System.Collections.Generic;
using System.Text;

namespace curd.mobile.Models.DTO
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime Creation { get; set; }
        public DateTime Modification { get; set; }
        public ProductDTO() { }
        public ProductDTO(Item item) 
        {
            Name = item.Name;
            Price = (decimal)item.Price;
            Creation = DateTime.Now;
            Modification = DateTime.Now;
        }

    }
}
