using Microsoft.EntityFrameworkCore;
using PlanBookingWebApp.data;
using PlanBookingWebApp.Models;

namespace PlanBookingWebApp.Repository.SqlRepo
{
    public class AirportRepository : IAirportRepository
    {
        ClassDbContext _dbContext;

        public AirportRepository(ClassDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Airport>> GetAirportsAsync()
        {
            return await _dbContext.airport.AsNoTracking().ToListAsync();
        }

        public async Task<Airport> AddAirportAsync(Airport airport)
        {
            await _dbContext.AddAsync(airport);
            await _dbContext.SaveChangesAsync();
            return airport;
        }

        public async Task<Airport> GetAirportByIdAsync(int id)
        {
            return await _dbContext.airport.AsNoTracking().FirstOrDefaultAsync(a => a.AirportId == id);
        }

        public async Task<Airport> UpdateAirportAsync(Airport ap)
        {
            _dbContext.airport.Attach(ap);
            _dbContext.Update(ap);
            await _dbContext.SaveChangesAsync();
            return ap;
        }

        public async Task<Airport> DeleteAirportAsync(int id)
        {
            // Asynchronously retrieve the speciality by Id
            var airport = await GetAirportByIdAsync(id);

            if (airport != null)
            {
                _dbContext.airport.Remove(airport);
                await _dbContext.SaveChangesAsync();

                return airport;
            }

            return airport;
        }


    }
}
