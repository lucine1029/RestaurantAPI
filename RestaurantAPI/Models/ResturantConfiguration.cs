using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class ResturantConfiguration
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public TimeSpan DefaultBookingDuration { get; set; }
        [Required]
        public TimeSpan BookingSlotInterval { get; set; }   //advanced!?!30min slots
        //public int CancellationPolicyHours { get; set; } // in hours
    }
}
