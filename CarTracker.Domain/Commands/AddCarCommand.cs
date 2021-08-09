using CarTracker.Domain.Events;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarTracker.Domain.Commands
{
    public class AddCarCommand : IRequest<DomainEvent[]>
    {
        public string ID { get; set; }

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
