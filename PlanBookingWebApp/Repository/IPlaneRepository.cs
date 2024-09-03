using PlanBookingWebApp.Models;
using System.Threading.Tasks;

namespace PlanBookingWebApp.Repository
{
    public interface IPlaneRepository
    {
        Task<List<Plane>> GetPlanesAsync();
        Task<Plane> AddPlaneAsync(Plane plane);

        Task<Plane> GetPlaneByIdAsync(int id);
        Task<Plane> UpdatePlaneAsync(Plane plane);
        Task<Plane> DeletePlaneAsync(int id);
    }
        
}
