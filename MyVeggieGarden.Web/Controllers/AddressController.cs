using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVeggieGarden.Models.Address;
using MyVeggieGarden.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVeggieGarden.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;

        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Address>> Create(AddressCreate addressCreate)
        {
            //int applicationUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            Console.WriteLine("Received obj from front-end and about to call repo insertAsync method");
           
            var createdAddress = await _addressRepository.InsertAsync(addressCreate);

            Console.WriteLine("Received createAddress from repo and about to send it to front-end: " + createdAddress.AddressCity);

            return (createdAddress);
        }
      
        [HttpGet("GetAddressById/{addressId}")]
        public async Task<ActionResult<Address>> GetAddressById(int addressId)
        {
            var address = await _addressRepository.GetAsync(addressId);

            return address;
        }

        [HttpGet("{streetNumber, streetName, zip}")]
        public async Task<ActionResult<Address>> GetAddress(string streetNumber, string streetName, string zip)
        {
            var address = await _addressRepository.GetByStNumNameZip(streetNumber, streetName, zip);

            return address;
        }
    }
}
