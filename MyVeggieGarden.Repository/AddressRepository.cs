using Dapper;
using Microsoft.Extensions.Configuration;
using MyVeggieGarden.Models.Address;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace MyVeggieGarden.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IConfiguration _config;

        public AddressRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task<Address> GetAsync(int addressId)
        {
            Address address;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                address = await connection.QueryFirstOrDefaultAsync<Address>("Address_GetById",
                                                                              new { AddressId = addressId },
                                                                              commandType: CommandType.StoredProcedure);
            }

            return address;
        }

        public async Task<Address> GetByStNumNameZip(string streetNumber, string streetName, string zip)
        {
            Address address;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                address = await connection.QueryFirstOrDefaultAsync<Address>("Address_GetByStNumNameZip",
                                                                        new { StreetNumber = streetNumber, StreetName = streetName, ZipCode = zip },
                                                                        commandType: CommandType.StoredProcedure);
            }

            return address;
        }

        public async Task<Address> InsertAsync(AddressCreate addressCreate)
        {
            Console.WriteLine("Received addressCreate obj in controller: " + addressCreate.AddressCity);

            var dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("AddressStreetNumber", typeof(string));
            dataTable.Columns.Add("AddressStreetName", typeof(string));
            dataTable.Columns.Add("AddressCity", typeof(string));
            dataTable.Columns.Add("AddressState", typeof(string));
            dataTable.Columns.Add("AddressZipCode", typeof(string));

            dataTable.Rows.Add(addressCreate.AddressStreetNumber, addressCreate.AddressStreetName, addressCreate.AddressCity,
                               addressCreate.AddressState, addressCreate.AddressZipCode);

            int newAddressId;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                Console.WriteLine("Controller called repo and repo is about to talk to db");

                newAddressId = await connection.ExecuteScalarAsync<int>("Address_Insert",
                                                                        new { Address = dataTable.AsTableValuedParameter("dbo.AddressType") },
                                                                        commandType: CommandType.StoredProcedure);
            }

            Console.WriteLine("Address created, about to call GetAsync with new addressId:" + newAddressId);

            Address address = await GetAsync(newAddressId);

            Console.WriteLine("Got address in repo and about to return it to controller: " + address.AddressCity);

            return address;
        }
    }
}
