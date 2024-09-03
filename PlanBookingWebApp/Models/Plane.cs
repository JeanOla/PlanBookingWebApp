using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PlanBookingWebApp.Models
{
    public class Plane
    {
        [Key]
        public int PlaneId { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Airline is required.")]
        [StringLength(100, ErrorMessage = "Airline name cannot be longer than 100 characters.")]
        public string Airline { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        [StringLength(50, ErrorMessage = "Model cannot be longer than 50 characters.")]
        public string Model { get; set; }

        [ValidateNever]
        public List<Flight> flight { get; set; }
    }
}
