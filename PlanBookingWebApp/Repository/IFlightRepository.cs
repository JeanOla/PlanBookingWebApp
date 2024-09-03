using PlanBookingWebApp.Models;

namespace PlanBookingWebApp.Repository
{
    public interface IFlightRepository
    {
        Task<List<Flight>> GetFlightAsync();
        Task<Flight> AddFlightAsync(Flight flight);
        Task<Flight> GetFlightByIdAsync(int id);
        Task<Flight> UpdateFlightAsync(Flight flight);
        Task<Flight> DeleteFlightAsync(int id);
        List<Airport> PopulateAirport();
        List<Plane> PopulatePlane();
    }
}
