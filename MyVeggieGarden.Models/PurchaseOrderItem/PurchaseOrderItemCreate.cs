using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyVeggieGarden.Models.PurchaseOrderItem
{
    public class PurchaseOrderItemCreate
    {
        [Required(ErrorMessage = "Purchase Order Id is required")]
        public int PurchaseOrderId { get; set; }

        [Required(ErrorMessage = "Product Id is required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity purchased is required")]
        public int ProductPurchasedQuantity { get; set; }
    }
}
