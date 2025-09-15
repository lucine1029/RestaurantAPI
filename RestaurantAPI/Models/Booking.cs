using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime BookingDate { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }     //30min slots ???
        public TimeSpan EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        [ForeignKey("Table")]
        public int FK_TableId { get; set; }
        [ForeignKey("Customer")]
        public int FK_CustomerId { get; set; }
        [Required]
        public int NumberOfGuests { get; set; }
        public string Status { get; set; } // e.g., "Pending", "Confirmed", "Cancelled"
        [MaxLength(200)]
        public string SpecialRequests { get; set; }
        [ForeignKey(nameof(FK_TableId))]
        public virtual Table Tables { get; set; }
        [ForeignKey(nameof(FK_CustomerId))]
        public virtual Customer Customers { get; set; }
    }
}
