using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVeggieGarden.Models.Card;
using MyVeggieGarden.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVeggieGarden.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;

        public CardController (ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        
        [HttpPost("Create")]
        public async Task<ActionResult<Card>> Create(CardCreate cardCreate)
        {
            Console.WriteLine("Inside Card Create controller; received cardCreate, cardCreate cardType: " + cardCreate.CardExDate);

            var createdCard = await _cardRepository.InsertAsync(cardCreate);

            Console.WriteLine("Card Created and received in controller and about to return, cardType: " + createdCard.CardExDate);

            return (createdCard);
        }

        [HttpGet("GetCardById/{cardId}")]
        public async Task<ActionResult<Card>> GetCardById(int cardId)
        {
            var card = await _cardRepository.GetAsync(cardId);

            return card;
        }

        [HttpGet("GetCardByNumber/{cardNumber}")]
        public async Task<ActionResult<Card>> GetCardByNumber(string cardNumber)
        {
            var card = await _cardRepository.GetByCardNumber(cardNumber);

            return card;
        }
    }
}
