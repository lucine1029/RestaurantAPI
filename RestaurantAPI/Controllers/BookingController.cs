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

        [HttpGet]
        [Route("/getavailableslots")]
        public async Task<ActionResult<List<AvailableTimeSlotsDTO>>>GetAvailableSlots([FromQuery] int numberOfGuests,[FromQuery]DateTime? date = null)
            {
            //if no date provided, use today as default
            var bookingDate = date ?? DateTime.Today;
            var availableSlots = await _bookingService.GetAvailableTimeSlotsAsync(bookingDate, numberOfGuests);
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

        //[HttpGet]
        //[Route("/getbookingsbyphone/{phoneNumber}")]  //lack of checking if phone number is valid or duplicate, need to add in service layer
        //public async Task<ActionResult<List<BookingDTO>>> GetBookingsByPhoneNumber(string phoneNumber)
        //{
        //    var bookings = await _bookingService.GetBookingsByCustomerPhoneAsync(phoneNumber);
        //    if (bookings == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(bookings);
        //}

        [HttpGet]
        [Route("/getallbookings")]
        public async Task<ActionResult<List<BookingDTO>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpPost]
        [Route("/getavailabletablesbyslot")]
        public async Task<ActionResult<List<TableDTO>>> GetAvailableTablesBySlot([FromBody] BookingCheckDTO bookingCheckDTO)
        {
            var availableTables = await _bookingService.GetAvailableTablesAsync(bookingCheckDTO);
            return Ok(availableTables);
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
                    error = ex.ErrorMessage,  
                    code = ex.ErrorCode
                });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the booking.");
            }
        }

        [HttpDelete]
        [Route("/deletebooking/{id}")]
        public async Task<ActionResult> DeleteBooking(int id)
        {
            try
            {
                var deletedBooking = await _bookingService.DeleteBookingAsync(id);
                deletedBooking.Message = string.Format(ApiMessages.Success.Deleted, "Booking");
                return Ok(deletedBooking);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new
                {
                    error = ex.ErrorMessage,  
                    code = ex.ErrorCode
                });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the booking.");
            }
        }

        [HttpPut]
        [Route("/updatebooking/{id}")]
        public async Task<ActionResult> UpdateBooking(int id, BookingUpdateDTO bookingUpdateDTO)
        {
            try
            {
                var updatedBooking = await _bookingService.UpdateBookingAsync(id, bookingUpdateDTO);
                updatedBooking.Message = string.Format(ApiMessages.Success.Updated, "Booking");
                return Ok(updatedBooking);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new
                {
                    error = ex.ErrorMessage, 
                    code = ex.ErrorCode
                });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the booking.");
            }
        }
    }
}
