using System;
using System.Collections.Generic;
using System.Text;

namespace MyVeggieGarden.Models.PurchaseOrder
{
    public class PurchaseOrder: PurchaseOrderCreate
    {
        public int PurchaseOrderId { get; set; }

        public int ApplicationUserId { get; set; }

        public string Username { get; set; }

        public DateTime PurchaseOrderDate { get; set; }

        public string AddressStreetNumber { get; set; }

        public string AddressStreetName { get; set; }

        public string AddressCity { get; set; }

        public string AddressState { get; set; }

        public string AddressZipCode { get; set; }

        public int CardNumber { get; set; }
    }
}
