using CarTracker.Domain.Events;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarTracker.Domain.Commands
{
    public class InspectBrakesCommand : IRequest<DomainEvent[]>
    {
        [Required]
        [MaxLength(50)]
        public string ID { get; set; }

        public int Mileage { get; set; }

        [Display(Name = "Remaining Pad")]
        public double RemainingPad { get; set; }
    }
}
