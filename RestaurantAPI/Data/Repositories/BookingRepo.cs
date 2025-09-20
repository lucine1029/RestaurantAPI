using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantAPI.Data.Repositories.IRepositories;
using RestaurantAPI.Models;
using RestaurantAPI.Models.DTOs.Booking;

namespace RestaurantAPI.Data.Repositories
{
    public class BookingRepo : IBookingRepo
    {
        private readonly ApplicationDbContext _context;
        public BookingRepo(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<int> CreateBookingAsync(Booking booking)
        {
            await _context.Booking.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking.Id;
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            var bookings = await _context.Booking.ToListAsync();
            return bookings;
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            var booking = await _context.Booking.FirstOrDefaultAsync(b => b.Id == id);
            return booking;
        }

        //public async Task<List<Booking>> GetBookingsByCustomerPhoneAsync(string phone)
        //{
        //    var booking = await _context.Booking.FirstOrDefaultAsync(b => b.);
        //    return booking;
        //}

        public async Task<List<Booking>> GetBookingsByDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HasOverLappingBookingAsync(int tableId, DateTime startTime, TimeSpan duration)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
