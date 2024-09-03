using Microsoft.AspNetCore.Mvc;
using PlanBookingWebApp.Models;
using PlanBookingWebApp.Repository;

namespace PlanBookingWebApp.Controllers
{
    public class AirportController : Controller
    {
        IAirportRepository _repo;

        public AirportController(IAirportRepository repo) 
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            var airportList = await _repo.GetAirportsAsync();
            return View(airportList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Airport airport)
        {
            var newAiport = await _repo.GetAirportsAsync();
            if (ModelState.IsValid)
            {
                if (!newAiport.Any(a => a.AirportName.Equals(airport.AirportName, StringComparison.OrdinalIgnoreCase)))
                {
                    await _repo.AddAirportAsync(airport);
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("AirportName", "Airport you entered is already existing.");
            return View(airport);
           
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid airport ID.");
            }

            var airport = await _repo.GetAirportByIdAsync(id);

            if (airport == null)
            {
                return NotFound($"Airport with ID {id} not found.");
            }

            return View(airport);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Airport airport)
        {
            if (airport == null)
            {
                return BadRequest("Invalid airport data.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.UpdateAirportAsync(airport);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the airport. Please try again.");
                }
            }
            return View(airport);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid airport ID.");
            }

            var airport = await _repo.GetAirportByIdAsync(id);
            if (airport == null)
            {
                return NotFound($"Airport with ID {id} not found.");
            }

            try
            {
                await _repo.DeleteAirportAsync(id);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the airport. Please try again.";

                return RedirectToAction("Index");
            }
            TempData["SuccessMessage"] = "Airport deleted successfully.";
            return RedirectToAction("Index");
        }


    }
}
