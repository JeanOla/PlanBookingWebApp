using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PlanBookingWebApp.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Passenger Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Passenger Name must be between 2 and 100 characters.")]
        [RegularExpression("^[a-zA-Z.'\\s]+$", ErrorMessage = "Only letters, periods, and apostrophes are allowed.")]
        public string PassengerName { get; set; }

        [ValidateNever] // This prevents model validation from running on this property
        public Flight Flight { get; set; }

        [Required(ErrorMessage = "Flight ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Flight ID must be a positive number.")]
        public int FlightId { get; set; }
    }
}
