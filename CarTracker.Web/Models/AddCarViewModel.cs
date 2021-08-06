using System.ComponentModel.DataAnnotations;

namespace CarTracker.Web.Models
{
    public class AddCarViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Make { get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        [Required]
        [MaxLength(50)]
        public string Owner { get; set; }
    }
}
