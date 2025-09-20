using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models.DTOs.Booking
{
    public class BookingDTO
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Table")]
        public int FK_TableId { get; set; }
        [ForeignKey("Customer")]
        public int FK_CustomerId { get; set; }

        //[ForeignKey(nameof(FK_TableId))]
        //public virtual Table Tables { get; set; }

        //[ForeignKey(nameof(FK_CustomerId))]
        //public virtual Customer Customers { get; set; }


        [Required]
        public DateTime BookingDate { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        //public TimeSpan EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        [Required]
        public int NumberOfGuests { get; set; }
        //public string Status { get; set; } = "Confirmed";
        public BookingStatus status { get; set; } = BookingStatus.Confirmed;
        [MaxLength(200)]
        public string SpecialRequests { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
    public enum BookingStatus
    {
        Confirmed,
        Cancelled,
        Pending
    }

}
