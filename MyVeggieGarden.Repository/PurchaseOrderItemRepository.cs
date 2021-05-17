using Dapper;
using Microsoft.Extensions.Configuration;
using MyVeggieGarden.Models.PurchaseOrderItem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVeggieGarden.Repository
{
    public class PurchaseOrderItemRepository : IPurchaseOrderItemRepository
    {
        IConfiguration _config;
        public PurchaseOrderItemRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<PurchaseOrderItem>> GetAll(int purchaseOrderId)
        {
            IEnumerable<PurchaseOrderItem> purchaseOrderItems;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                purchaseOrderItems = await connection.QueryAsync<PurchaseOrderItem>("PurchaseOrderItem_GetByOrderId",
                                                                                    new { PurchaseOrderId = purchaseOrderId }, 
                                                                                    commandType: CommandType.StoredProcedure);
            }

            return purchaseOrderItems.ToList();
        }

        
        public async Task<PurchaseOrderItemCreate> InsertAsync(int purchaseOrderId, int productId, int productPurchasedQuantity)
        {
            PurchaseOrderItemCreate newPurchaseOrderItem;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                                     
                newPurchaseOrderItem = await connection.QueryFirstOrDefaultAsync<PurchaseOrderItemCreate>("PurchaseOrderItem_Insert",
                                                                                new
                                                                                {
                                                                                    PurchaseOrderId = purchaseOrderId,
                                                                                    ProductId = productId,
                                                                                    ProductPurchasedQuantity = productPurchasedQuantity
                                                                                },
                                                                                commandType: CommandType.StoredProcedure);
            }

            return newPurchaseOrderItem;
        }
        
    }
}
