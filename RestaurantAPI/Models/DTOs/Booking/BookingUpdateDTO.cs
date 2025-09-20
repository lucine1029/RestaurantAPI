namespace RestaurantAPI.Models.DTOs.Booking
{
    public class BookingUpdateDTO
    {
        public DateTime? BookingDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public int NumberOfGuests { get; set; }
        public int TableId { get; set; }
        public BookingStatus status { get; set; }
        public string SpecialRequests { get; set; }
    }
}
