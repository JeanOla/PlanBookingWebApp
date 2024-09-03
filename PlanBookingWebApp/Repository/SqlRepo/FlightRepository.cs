using Microsoft.EntityFrameworkCore;
using PlanBookingWebApp.data;
using PlanBookingWebApp.Models;

namespace PlanBookingWebApp.Repository.SqlRepo
{
    public class FlightRepository : IFlightRepository
    {
        ClassDbContext _dbContext;

        public FlightRepository(ClassDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Flight>> GetFlightAsync()
        {
            return await _dbContext.flight.Include(a => a.airport).Include(p =>p.plane).AsNoTracking().ToListAsync();
        }

        public async Task<Flight> AddFlightAsync(Flight flight)
        {
            await _dbContext.AddAsync(flight);
            await _dbContext.SaveChangesAsync();
            return flight;
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            return await _dbContext.flight.Include(a => a.airport).Include(p => p.plane).AsNoTracking().FirstOrDefaultAsync(a => a.FlightId == id);
        }

        public async Task<Flight> UpdateFlightAsync(Flight flight)
        {
            _dbContext.flight.Attach(flight);
            _dbContext.Update(flight);
            await _dbContext.SaveChangesAsync();
            return flight;
        }

        public async Task<Flight> DeleteFlightAsync(int id)
        {
            var flight = await GetFlightByIdAsync(id);

            if (flight != null)
            {
                _dbContext.flight.Remove(flight);
                await _dbContext.SaveChangesAsync();

                return flight;
            }

            return flight;
        }
        public List<Airport> PopulateAirport()
        {
            return _dbContext.airport.AsNoTracking().ToList();
        }
        public List<Plane> PopulatePlane()
        {
            return _dbContext.plane.AsNoTracking().ToList();
        }
    }
}
