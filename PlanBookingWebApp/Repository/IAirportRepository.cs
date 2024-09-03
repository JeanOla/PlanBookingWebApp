using PlanBookingWebApp.Models;

namespace PlanBookingWebApp.Repository
{
    public interface IAirportRepository
    {
        Task<List<Airport>> GetAirportsAsync();

        Task<Airport> AddAirportAsync(Airport airport);
        Task<Airport> GetAirportByIdAsync(int id);
        Task<Airport> UpdateAirportAsync(Airport airport);

        Task<Airport> DeleteAirportAsync(int id);
    }
}
