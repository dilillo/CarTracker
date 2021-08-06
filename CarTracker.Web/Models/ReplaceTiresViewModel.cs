using System.ComponentModel.DataAnnotations;

namespace CarTracker.Web.Models
{
    public class ReplaceTiresViewModel
    {
        [Required]
        [MaxLength(50)]
        public string ID { get; set; }

        public int NumberOfTiresReplaced { get; set; }

        public decimal Charge { get; set; }
    }
}
