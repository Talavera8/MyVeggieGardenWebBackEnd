using System;
using System.Collections.Generic;
using System.Text;

namespace MyVeggieGarden.Models.PurchaseOrderItem
{
    public class PurchaseOrderItem
    {
        public int PurchaseOrderId { get; set; }

        public int ApplicationUserId { get; set; }

        public string Username { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int ProductPurchasedQuantity { get; set; }
    }
}
