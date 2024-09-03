using Microsoft.EntityFrameworkCore;
using PlanBookingWebApp.data;
using PlanBookingWebApp.Models;

namespace PlanBookingWebApp.Repository.SqlRepo
{
    public class BookingtRepository : IBookingRepository
    {
        ClassDbContext _dbContext;

        public BookingtRepository(ClassDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Booking>> GetBookingAsync()
        {
            return await _dbContext.booking.Include(a => a.Flight).AsNoTracking().ToListAsync();
        }

        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            await _dbContext.AddAsync(booking);
            await _dbContext.SaveChangesAsync();
            return booking;
        }
      //  Booking
        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            return await _dbContext.booking.Include(a => a.Flight).AsNoTracking().FirstOrDefaultAsync(a => a.BookingId == id);
        }

        public async Task<Booking> UpdateBookingAsync(Booking booking)
        {
            _dbContext.booking.Attach(booking);
            _dbContext.Update(booking);
            await _dbContext.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking> DeleteBookingAsync(int id)
        {
            var booking = await GetBookingByIdAsync(id);

            if (booking != null)
            {
                _dbContext.booking.Remove(booking);
                await _dbContext.SaveChangesAsync();

                return booking;
            }

            return booking;
        }
        public List<Flight> PopulateFlight()
        {
            return _dbContext.flight.AsNoTracking().ToList();
        }
    }
}
