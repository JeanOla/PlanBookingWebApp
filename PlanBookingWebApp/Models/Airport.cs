using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanBookingWebApp.Models
{
    public class Airport
    {
        public int AirportId { get; set; }
        [Required(ErrorMessage = "Airport Name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Airport Name must be between 3 and 100 characters.")]
        public string AirportName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
        public string Address { get; set; }

        [ValidateNever]
        public List<Flight> flight { get; set; }
    }
}
