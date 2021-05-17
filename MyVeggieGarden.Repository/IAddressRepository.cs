using MyVeggieGarden.Models.Address;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyVeggieGarden.Repository
{
    public interface IAddressRepository
    {
        public Task<Address> InsertAsync(AddressCreate addressCreate);

        public Task<Address> GetAsync(int addressId);

        public Task<Address> GetByStNumNameZip(string streetNumber, string streetName, string zip);
    }
}
