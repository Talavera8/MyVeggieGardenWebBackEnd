using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyVeggieGarden.Models.PurchaseOrder
{
    public class PurchaseOrderCreate
    {
        [Required(ErrorMessage = "Total Amount is required")]
        public double PurchaseOrderTotalAmount { get; set; }
        public int AddressId { get; set; }

        public int CardId { get; set; }
    }
}
