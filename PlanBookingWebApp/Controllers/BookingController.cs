using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlanBookingWebApp.Models;
using PlanBookingWebApp.Repository;

namespace PlanBookingWebApp.Controllers
{
    public class BookingController : Controller
    {
        IBookingRepository _repo;
        public BookingController(IBookingRepository repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var bookinglist = await _repo.GetBookingAsync();

                if (bookinglist == null || !bookinglist.Any())
                {
                    TempData["InfoMessage"] = "No bookings available at the moment.";
                }
                return View(bookinglist);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while retrieving the bookings. Please try again later.";
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
           
                ViewBag.Flight = new SelectList(_repo.PopulateFlight(), "FlightId", "FlightCode");

                var booking = new Booking();

                return View(booking);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while preparing the create booking form. Please try again later.";

                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Booking booking)
        {
            ViewBag.flight = new SelectList(_repo.PopulateFlight(), "FlightId", "FlightCode");
            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.AddBookingAsync(booking);
                    TempData["SuccessMessage"] = "Booking created successfully.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "An error occurred while creating the booking. Please try again later.";
                    return View(booking);
                }
            }

            return View(booking);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.flight = new SelectList(_repo.PopulateFlight(), "FlightId", "FlightCode");

            if (id <= 0)
            {
                return BadRequest("Invalid booking ID.");
            }

            var booking = await _repo.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound($"Booking with ID {id} not found.");
            }

            return View(booking);
        }


        [HttpPost]
        public async Task<IActionResult> Update(Booking booking)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.UpdateBookingAsync(booking);
                    TempData["SuccessMessage"] = "Booking updated successfully.";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "An error occurred while updating the booking. Please try again later.";
                }
            }

            ViewBag.flight = new SelectList(_repo.PopulateFlight(), "FlightId", "FlightCode");
            return View(booking);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid booking ID.");
            }

            try
            {
                var booking = await _repo.GetBookingByIdAsync(id);
                if (booking == null)
                {
                    return NotFound($"Booking with ID {id} not found.");
                }

                await _repo.DeleteBookingAsync(id);
                TempData["SuccessMessage"] = "Booking deleted successfully.";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the booking. Please try again later.";
            }

            return RedirectToAction("Index");
        }

    }
}
