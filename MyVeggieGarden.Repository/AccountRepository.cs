using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MyVeggieGarden.Models.Account;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace MyVeggieGarden.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration _config;

        public AccountRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            Console.WriteLine("Inside CreateAsync method of AccountRepository");

            cancellationToken.ThrowIfCancellationRequested();

            var dataTable = new DataTable();

            dataTable.Columns.Add("Username", typeof(string));
            dataTable.Columns.Add("NormalizedUsername", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("NormalizedEmail", typeof(string));
            dataTable.Columns.Add("Fullname", typeof(string));
            dataTable.Columns.Add("PasswordHash", typeof(string));

            dataTable.Rows.Add(
                user.Username,
                user.NormalizedUsername,
                user.Email,
                user.NormalizedEmail,
                user.Fullname,
                user.PasswordHash
                );

            System.Diagnostics.Debug.WriteLine("dataTable created; about to connect to db");

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync(cancellationToken);

                Console.WriteLine("About to execute Account_Insert procedure");

                await connection.ExecuteAsync("Account_Insert",
                    new { Account = dataTable.AsTableValuedParameter("dbo.AccountType") }, commandType: CommandType.StoredProcedure);

                Console.WriteLine("Procedure executed");
            }

            Console.WriteLine("About to return insert result");

            return IdentityResult.Success;
        }

        public async Task<ApplicationUserIdentity> GetByUsernameAsync(string normalizedUsername, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ApplicationUserIdentity applicationUser;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync(cancellationToken);

                applicationUser = await connection.QuerySingleOrDefaultAsync<ApplicationUserIdentity>(
                    "Account_GetByUsername", new { NormalizedUsername = normalizedUsername },
                    commandType: CommandType.StoredProcedure
                    );
            }

            return applicationUser;
        }
    }
}