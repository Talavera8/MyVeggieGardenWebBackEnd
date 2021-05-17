using MyVeggieGarden.Models.PurchaseOrderItem;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyVeggieGarden.Repository
{
    public interface IPurchaseOrderItemRepository
    {

        public Task<PurchaseOrderItemCreate> InsertAsync(int purchaseOrderId, int productId, int producturchasedQuantity);

        public Task<List<PurchaseOrderItem>> GetAll(int purchaseOrderId);
    }
}
