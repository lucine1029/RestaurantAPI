namespace RestaurantAPI.Models.DTOs.Booking
{
    public class BookingMessageDTO
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public int TableName { get; set; }
        public int TableCapacity { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone {  get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;

        public string ConfirmationMessage {  get; set; } = string.Empty;
    }
}
