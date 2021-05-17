using MyVeggieGarden.Models.Card;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyVeggieGarden.Repository
{
    public interface ICardRepository
    {
        public Task<Card> InsertAsync(CardCreate cardCreate);

        public Task<Card> GetAsync(int cardId);

        public Task<Card> GetByCardNumber(string cardNumber);
    }
}
