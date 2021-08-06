using System.ComponentModel.DataAnnotations;

namespace CarTracker.Web.Models
{
    public class ChangeOilViewModel
    {
        [Required]
        [MaxLength(50)]
        public string ID { get; set; }

        public int Mileage { get; set; }

        public decimal Charge { get; set; }
    }
}
