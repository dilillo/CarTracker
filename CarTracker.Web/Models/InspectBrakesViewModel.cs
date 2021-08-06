using System.ComponentModel.DataAnnotations;

namespace CarTracker.Web.Models
{
    public class InspectBrakesViewModel
    {
        [Required]
        [MaxLength(50)]
        public string ID { get; set; }

        public int Mileage { get; set; }

        public double RemainingPad { get; set; }
    }
}
