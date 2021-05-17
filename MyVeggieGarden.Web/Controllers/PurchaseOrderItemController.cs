using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVeggieGarden.Models.PurchaseOrderItem;
using MyVeggieGarden.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVeggieGarden.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderItemController : ControllerBase
    {
        private readonly IPurchaseOrderItemRepository _purchaseOrderItemRepository;

        public PurchaseOrderItemController(IPurchaseOrderItemRepository purchaseOrderItemRepository)
        {
            _purchaseOrderItemRepository = purchaseOrderItemRepository;
        }

        [HttpPost("{purchaseOrderId, productId, productPurchasedQuantity}")]
        public async Task<ActionResult<PurchaseOrderItemCreate>> Create(int purchaseOrderId, int productId, int productPurchasedQuantity)
        {
            var itemCreated = await _purchaseOrderItemRepository.InsertAsync(purchaseOrderId, productId, productPurchasedQuantity);

            return itemCreated;

        }

        [HttpGet("{purchaseOrderId}")]
        public async Task<ActionResult<List<PurchaseOrderItem>>> GetAllItems(int purchaseOrderId)
        {
            var purchaseOrderItems = await _purchaseOrderItemRepository.GetAll(purchaseOrderId);

            return purchaseOrderItems;
        }
    }
}
