using CarTracker.Domain.Events;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarTracker.Domain.Commands
{
    public class NewOwnerCommand : IRequest<DomainEvent[]>
    {
        [Required]
        [MaxLength(50)]
        public string ID { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name ="New Owner")]
        public string NewOwner { get; set; }
    }
}
