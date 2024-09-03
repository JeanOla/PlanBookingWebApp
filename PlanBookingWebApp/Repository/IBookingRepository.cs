using PlanBookingWebApp.Models;

namespace PlanBookingWebApp.Repository
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetBookingAsync();
        Task<Booking> AddBookingAsync(Booking booking);
        Task<Booking> GetBookingByIdAsync(int id);
        Task<Booking> UpdateBookingAsync(Booking booking);
        Task<Booking> DeleteBookingAsync(int id);
        List<Flight> PopulateFlight();
    }
}
