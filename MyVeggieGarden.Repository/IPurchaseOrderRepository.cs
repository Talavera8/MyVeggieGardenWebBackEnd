using MyVeggieGarden.Models.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyVeggieGarden.Repository
{
    public  interface IPurchaseOrderRepository
    {
        public Task<PurchaseOrder> GetAsync(int purchaseOrderId);

        public Task<int> InsertAsync(int applicationUserId, PurchaseOrderCreate purchOrderCreate);

        public Task<List<PurchaseOrder>> GetAllByUserId(int applicationUserId);

        public Task<int> DeleteAsync(int purchaseOrderId);

    }
}
