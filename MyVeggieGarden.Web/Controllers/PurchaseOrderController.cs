using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVeggieGarden.Models.PurchaseOrder;
using MyVeggieGarden.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MyVeggieGarden.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public PurchaseOrderController(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create(PurchaseOrderCreate purchOrderCreate)
        {
            int applicationUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var createdPurchaseOrderId = await _purchaseOrderRepository.InsertAsync(applicationUserId, purchOrderCreate);

            return createdPurchaseOrderId;
        }

        [HttpGet("{purchaseOrderId}")]
        public async Task<ActionResult<PurchaseOrder>> GetByOrderId(int purchaseOrderId)
        {
            var purchaseOrder = await _purchaseOrderRepository.GetAsync(purchaseOrderId);

            return purchaseOrder;
        }

        [HttpGet]
        public async Task<ActionResult<List<PurchaseOrder>>> GetAllOrdersByUserId()
        {
            int applicationUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var purchaseOrders = await _purchaseOrderRepository.GetAllByUserId(applicationUserId);

            return purchaseOrders;
        }

        [HttpDelete("{purchaseOrderId}")]
        public async Task<ActionResult<int>> Delete(int purchaseOrderId)
        {
            int applicationUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var foundPurchaseOrder = await _purchaseOrderRepository.GetAsync(purchaseOrderId);

            if (foundPurchaseOrder == null)
            {
                return BadRequest("Purchase Order does not exist");
            }

            if (foundPurchaseOrder.ApplicationUserId == applicationUserId)
            {
                var affectedRows = await _purchaseOrderRepository.DeleteAsync(purchaseOrderId);

                return (affectedRows);
            }
            else
            {
                return BadRequest("This purchase order was not created by the current user.");
            }
        }      
    }
}
