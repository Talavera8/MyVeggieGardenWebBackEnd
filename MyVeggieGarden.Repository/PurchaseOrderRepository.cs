using Dapper;
using Microsoft.Extensions.Configuration;
using MyVeggieGarden.Models.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVeggieGarden.Repository
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly IConfiguration _config;
        public PurchaseOrderRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<int> DeleteAsync(int purchaseOrderId)
        {
            int affectedRows = 0;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                //statement to retrieve affectedRows is not in the procedure; it's automatically available by delete procedure
                affectedRows = await connection.ExecuteScalarAsync<int>("PurchaseOrder_Delete",
                                                                        new { PurchaseOrderId = purchaseOrderId },
                                                                        commandType: System.Data.CommandType.StoredProcedure);
            }

            return affectedRows;
        }

        public async Task<List<PurchaseOrder>> GetAllByUserId(int applicationUserId)
        {
            IEnumerable<PurchaseOrder> purchaseOrders;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                purchaseOrders = await connection.QueryAsync<PurchaseOrder>("PurchaseOrder_GetByUserId",
                                                                           new { ApplicationUserId = applicationUserId },
                                                                           commandType: CommandType.StoredProcedure);
            }

            return purchaseOrders.ToList();
        }

        public async Task<PurchaseOrder> GetAsync(int purchaseOrderId)
        {
            PurchaseOrder purchaseOrder;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                purchaseOrder = await connection.QueryFirstOrDefaultAsync<PurchaseOrder>("PurchaseOrder_GetById",
                                                                           new { PurchaseOrderId = purchaseOrderId },
                                                                           commandType: CommandType.StoredProcedure);
            }

            return purchaseOrder;
        }

        public async Task<int> InsertAsync(int applicationUserId, PurchaseOrderCreate purchOrderCreate)
        {
            int newPurchaseOrderId;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                newPurchaseOrderId = await connection.ExecuteScalarAsync<int>("PurchaseOrder_Insert",
                                                                            new
                                                                            {
                                                                                ApplicationUserId = applicationUserId,
                                                                                PurchaseOrderTotalAmount = purchOrderCreate.PurchaseOrderTotalAmount,
                                                                                DeliveryAddressId = purchOrderCreate.AddressId,
                                                                                CardId = purchOrderCreate.CardId
                                                                            },
                                                                            commandType: CommandType.StoredProcedure);
            }

           //PurchaseOrder purchaseOrder = await GetAsync(newPurchaseOrderId);

            return newPurchaseOrderId;
        }
    }
}
