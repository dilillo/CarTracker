using CarTracker.Domain.Events;
using MediatR;

namespace CarTracker.Domain.Commands
{
    public class AddCarCommand : IRequest<DomainEvent[]>
    {
        public string ID { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Owner { get; set; }
    }
}
