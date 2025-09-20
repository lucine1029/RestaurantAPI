using RestaurantAPI.Models.DTOs.Booking;

namespace RestaurantAPI.Services.IServices
{
    public interface IBookingService
    {
        //for customer-related
        Task<List<AvailableTimeSlotsDTO>> GetAvailableTimeSlotsAsync(DateTime date, int numberOfGuests);
        Task<BookingMessageDTO> CreateBookingAsync(BookingCreateDTO bookingCreateDTO);


        //for admin-only
        Task<BookingDTO> GetBookingByIdAsync (int id);
        Task<BookingDTO> GetBookingByCustomerPhoneAsync(string phone);
        Task<List<BookingDTO>> GetBookingsByDateAsync(DateTime date);
        Task<BookingMessageDTO> UpdateBookingAsync(int id, BookingUpdateDTO bookingUpdateDTO);
        Task<BookingMessageDTO> DeleteBookingAsync(int id);

    }
}
