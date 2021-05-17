using System;
using System.Collections.Generic;
using System.Text;

namespace MyVeggieGarden.Models.Product
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public string ProductImageUrl { get; set; }

        public string ProductSellingUnit { get; set; }

        public decimal ProductUnitPrice { get; set; }

        public int ProductQuantityAvailable { get; set; }
    }
}
