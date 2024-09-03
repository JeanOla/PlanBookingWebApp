using Microsoft.AspNetCore.Mvc;
using PlanBookingWebApp.Models;
using PlanBookingWebApp.Repository;

namespace PlanBookingWebApp.Controllers
{
    public class PlaneController : Controller
    {
        IPlaneRepository _repo;

        public PlaneController(IPlaneRepository repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            var planeList = await _repo.GetPlanesAsync();
            return View(planeList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Plane plane)
        {
            if (!ModelState.IsValid)
            {
                return View(plane);
            }

            var existingPlanes = await _repo.GetPlanesAsync();

            if (existingPlanes.Any(p => p.Code.Equals(plane.Code, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError("Code", "A plane with this code already exists.");
                return View(plane);
            }

            try
            {
                await _repo.AddPlaneAsync(plane);
            }
            catch (Exception ex)
            {
                // Log the exception
                ModelState.AddModelError("", $"An error occurred while creating the plane: {ex.Message}");
                return View(plane);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var plane = await _repo.GetPlaneByIdAsync(id);

            if (plane == null)
            {
                return NotFound();
            }

            return View(plane);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Plane plane)
        {
            if (!ModelState.IsValid)
            {
                return View(plane);
            }

            try
            {
                await _repo.UpdatePlaneAsync(plane);
            }
            catch (Exception ex)
            {
                // Log the exception
                ModelState.AddModelError("", $"An error occurred while updating the plane: {ex.Message}");
                return View(plane);
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repo.DeletePlaneAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception
                ModelState.AddModelError("", $"An error occurred while deleting the plane: {ex.Message}");
            }

            return RedirectToAction("Index");
        }
    }
}