using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyVeggieGarden.Models.Card
{
    public class CardCreate
    {
        [Required(ErrorMessage = "Card type is required")]
        [MinLength(2, ErrorMessage = "Must be at least 2-5 characters")]
        [MaxLength(5, ErrorMessage = "Must be at least 2-5 characters")]
        public string CardType { get; set; }

        [MinLength(13, ErrorMessage = "Must be at least 13-19 characters")]
        [MaxLength(19, ErrorMessage = "Must be at least 13-19 characters")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Name on card is required")]
        [MinLength(3, ErrorMessage = "Must be at least 3-25 characters")]
        [MaxLength(25, ErrorMessage = "Must be at least 3-25 characters")]
        public string NameOnCard { get; set; }

        [Required(ErrorMessage = "Expiration date is required")]
        
        public DateTime CardExDate { get; set; }

        [Required(ErrorMessage = "CVV number is required")]
        [Range(001, 9999, ErrorMessage = "CVV must be 3-4 integers")]
        public int CardCVV { get; set; }

        public double CardBalance { get; set; }
    }
}
