using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlanBookingWebApp.Models;
using PlanBookingWebApp.Repository;
using System;

namespace PlanBookingWebApp.Controllers
{
    public class FlightController : Controller
    {
        IFlightRepository _repo;

        public FlightController(IFlightRepository repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var flightList = await _repo.GetFlightAsync();
                return View(flightList);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while retrieving the flight list. Please try again later.";
                return View("Error"); 
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                ViewBag.Airport = new SelectList(_repo.PopulateAirport(), "AirportId", "AirportName");
                ViewBag.Plane = new SelectList(_repo.PopulatePlane(), "PlaneId", "Code");
                var flight = new Flight();
                return View(flight);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while preparing the create flight form. Please try again later.";
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Flight flight)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Airport = new SelectList(_repo.PopulateAirport(), "AirportId", "AirportName");
                ViewBag.Plane = new SelectList(_repo.PopulatePlane(), "PlaneId", "Code");
                return View(flight);
            }

            try
            {
                await _repo.AddFlightAsync(flight);
                TempData["SuccessMessage"] = "Flight created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the flight. Please try again later.";
                ViewBag.Airport = new SelectList(_repo.PopulateAirport(), "AirportId", "AirportName");
                ViewBag.Plane = new SelectList(_repo.PopulatePlane(), "PlaneId", "Code");
                return View(flight);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid flight ID.");
            }

            try
            {
                ViewBag.Airport = new SelectList(_repo.PopulateAirport(), "AirportId", "AirportName");
                ViewBag.Plane = new SelectList(_repo.PopulatePlane(), "PlaneId", "Code");
                var flight = await _repo.GetFlightByIdAsync(id);

                if (flight == null)
                {
                    return NotFound($"Flight with ID {id} not found.");
                }

                return View(flight);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while preparing the update flight form. Please try again later.";
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(Flight flight)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.UpdateFlightAsync(flight);
                    TempData["SuccessMessage"] = "Flight updated successfully.";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "An error occurred while updating the flight. Please try again later.";
                }
            }

            ViewBag.Airport = new SelectList(_repo.PopulateAirport(), "AirportId", "AirportName");
            ViewBag.Plane = new SelectList(_repo.PopulatePlane(), "PlaneId", "Code");
            return View(flight);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid flight ID.");
            }

            try
            {
                var flight = await _repo.GetFlightByIdAsync(id);

                if (flight == null)
                {
                    return NotFound($"Flight with ID {id} not found.");
                }

                await _repo.DeleteFlightAsync(id);
                TempData["SuccessMessage"] = "Flight deleted successfully.";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the flight. Please try again later.";
            }

            return RedirectToAction("Index");
        }

    }
}
