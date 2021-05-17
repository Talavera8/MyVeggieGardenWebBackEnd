using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyVeggieGarden.Models.Address
{
    public class AddressCreate
    {
        [Required(ErrorMessage = "Street address is required")]
        [MaxLength(10, ErrorMessage = "Must be at least 1-10 characters")]
        [MinLength(1, ErrorMessage = "Must be at least 1-10 characters")]
        public string AddressStreetNumber { get; set; }

        [Required(ErrorMessage = "Street name is required")]
        [MaxLength(25, ErrorMessage = "Must be at least 5-25 characters")]
        [MinLength(5, ErrorMessage = "Must be at least 5-25 characters")]
        public string AddressStreetName { get; set; }

        [Required(ErrorMessage = "City is required")]
        [MaxLength(25, ErrorMessage = "Must be at least 1-25 characters")]
        [MinLength(3, ErrorMessage = "Must be at least 1-25 characters")]
        public string AddressCity { get; set; }

        [Required(ErrorMessage = "State is required")]
        [MaxLength(10, ErrorMessage = "Must be at least 4-10 characters")]
        [MinLength(4, ErrorMessage = "Must be at least 4-10 characters")]
        public string AddressState { get; set; }

        [MaxLength(5, ErrorMessage = "Must be 5 digits")]
        [MinLength(5, ErrorMessage = "Must be 5 digits")]
        [Required(ErrorMessage = "Zip Code is required")]
        public string AddressZipCode { get; set; }
    }
}
