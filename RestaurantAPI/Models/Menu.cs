using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public int Price { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        [Required]
        public bool IsVegan { get; set; }
        [Required]
        public bool HasNuts { get; set; }
        [Required]
        public bool HasEgg { get; set; }



        //[Required]
        //public bool HasDairy { get; set; }
        //[Required]
        //public bool IsSpicy { get; set; }
        //[Required]
        //public bool IsGlutenFree { get; set; }
    }
}
