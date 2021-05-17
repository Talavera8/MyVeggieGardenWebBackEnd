using Dapper;
using Microsoft.Extensions.Configuration;
using MyVeggieGarden.Models.Card;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace MyVeggieGarden.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly IConfiguration _config;

        public CardRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<Card> GetAsync(int cardId)
        {
            Card card;

            Console.WriteLine("Inside GetAsync method; received new cardId: " + cardId);

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                card = await connection.QueryFirstOrDefaultAsync<Card>("Card_GetById",
                                                                 new { CardId = cardId },     
                                                                 commandType: CommandType.StoredProcedure);
                Console.WriteLine("Received card after connecting to db from repo, cardType: " + card.CardType);
            }

            Console.WriteLine("About to return card to controller");

            return card;
        }

        public async Task<Card> GetByCardNumber(string cardNumber)
        {
            Card card;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                card = await connection.QueryFirstOrDefaultAsync<Card>("Card_GetByNumber",
                                                                 new { CardNumber = cardNumber},
                                                                 commandType: CommandType.StoredProcedure);
            }

            return card;
        }

        public async Task<Card> InsertAsync(CardCreate cardCreate)
        {
            Console.WriteLine("Inside InsertAsync of repository, received cardCreate obj, cardType: " + cardCreate.CardExDate);
            
            var dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("CardType", typeof(string));
            dataTable.Columns.Add("CardNumber", typeof(string));
            dataTable.Columns.Add("NameOnCard", typeof(string));
            dataTable.Columns.Add("CardExpDate", typeof(DateTime));
            dataTable.Columns.Add("CardCVV", typeof(string));
            dataTable.Columns.Add("CardBalance", typeof(double));

            dataTable.Rows.Add(cardCreate.CardType, cardCreate.CardNumber, cardCreate.NameOnCard, cardCreate.CardExDate, cardCreate.CardCVV, cardCreate.CardBalance);

            int newCardId;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                
                await connection.OpenAsync();

                Console.WriteLine("About to call stored procedure");

                newCardId = await connection.ExecuteScalarAsync<int>("Card_Insert",
                                                                     new {Card = dataTable.AsTableValuedParameter("dbo.CardType") },
                                                                     commandType: CommandType.StoredProcedure);

                Console.WriteLine("id for new cardId received: " + newCardId);
            }

            Console.WriteLine("About to call repository again to get new card by id returned");

            Card card = await GetAsync(newCardId);

            Console.WriteLine("card returned: " + card.CardExDate);

            return card;
        }
    }
}
