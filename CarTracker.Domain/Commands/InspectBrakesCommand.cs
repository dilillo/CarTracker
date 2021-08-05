using CarTracker.Domain.Events;
using MediatR;

namespace CarTracker.Domain.Commands
{
    public class InspectBrakesCommand : IRequest<DomainEvent[]>
    {
        public string ID { get; set; }

        public int Mileage { get; set; }

        public double RemainingPad { get; set; }
    }
}
