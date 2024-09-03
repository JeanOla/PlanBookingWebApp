using Microsoft.EntityFrameworkCore;
using PlanBookingWebApp.data;
using PlanBookingWebApp.Models;

namespace PlanBookingWebApp.Repository.SqlRepo
{
    public class PlaneRepository : IPlaneRepository
    {
        ClassDbContext _dbContext;
        public PlaneRepository(ClassDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Plane>> GetPlanesAsync()
        {
            return await _dbContext.plane.AsNoTracking().ToListAsync();
        }
        public async Task<Plane> AddPlaneAsync(Plane plane)
        {
            await _dbContext.AddAsync(plane);
            await _dbContext.SaveChangesAsync();
            return plane;
        }
        public async Task<Plane> GetPlaneByIdAsync(int id)
        {
            return await _dbContext.plane.AsNoTracking().FirstOrDefaultAsync(a => a.PlaneId == id);
        }
        public async Task<Plane> UpdatePlaneAsync(Plane plane)
        {
            _dbContext.plane.Attach(plane);
            _dbContext.Update(plane);
            await _dbContext.SaveChangesAsync();
            return plane;
        }

        public async Task<Plane> DeletePlaneAsync(int id)
        {
          
            var plane = await GetPlaneByIdAsync(id);

            if (plane != null)
            {
                _dbContext.plane.Remove(plane);
                await _dbContext.SaveChangesAsync();

                return plane;
            }

            return plane;
        }

    }
}
