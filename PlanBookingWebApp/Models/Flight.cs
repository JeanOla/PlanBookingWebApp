using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanBookingWebApp.Models
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }
        [Required(ErrorMessage = "Flight Code is required.")]
        public string FlightCode { get; set; }
        [ValidateNever]
        public Airport airport { get; set; }
        [Required(ErrorMessage = "Airport ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Airport must be filled.")]
        public int AirportId { get; set; }
        [ValidateNever]
        public Plane plane { get; set; }
        [Required(ErrorMessage = "Plane ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Plane  must be filled.")]
        public int PlaneId { get; set; }
        [ValidateNever]
        public List<Booking> booking { get; set; }
        [Required(ErrorMessage = "PIlot name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Passenger Name must be between 2 and 100 characters.")]
        [RegularExpression("^[a-zA-Z.'\\s]+$", ErrorMessage = "Only letters, periods, and apostrophes are allowed.")]
        public string Pilot { get; set; }

    }
}
