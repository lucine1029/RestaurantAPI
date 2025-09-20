using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Constants;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models.DTOs.Booking;
using RestaurantAPI.Models.DTOs.Table;
using RestaurantAPI.Services;
using RestaurantAPI.Services.IServices;
using System;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        [Route("/getavailableslots")]
        public async Task<ActionResult<List<BookingDTO>>>GetAvailableSlots([FromBody] DateTime date, int numberOfGuests)
            {
            var availableSlots = await _bookingService.GetAvailableTimeSlotsAsync(date, numberOfGuests);
            return Ok(availableSlots);
            }


        [HttpGet]
        [Route("/getbookingbyid/{id}")]
        public async Task<ActionResult<BookingDTO>> GetBookingById(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpPost]
        [Route("/createbooking")]
        public async Task<ActionResult<BookingCreateDTO>> CreateBookingAsync([FromBody] BookingCreateDTO bookingCreateDTO)
        {
            try
            {
                var newBooking = await _bookingService.CreateBookingAsync(bookingCreateDTO);
                return CreatedAtAction(nameof(GetBookingById), new { id = newBooking.BookingId }, newBooking);

            }
            catch (ValidationException ex)
            {
                return BadRequest(new
                {
                    error = ex.ErrorMessage,  //the customered message and code is from the tableService
                    code = ex.ErrorCode
                });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the booking.");
            }
        }
    }
}
