using CarTracker.Domain.Events;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarTracker.Domain.Commands
{
    public class ReplaceTiresCommand : IRequest<DomainEvent[]>
    {
        [Required]
        [MaxLength(50)]
        public string ID { get; set; }

        [Display(Name = "Number of Tires Replaced")]
        public int NumberOfTiresReplaced { get; set; }

        public decimal Charge { get; set; }
    }
}
