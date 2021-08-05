using CarTracker.Domain.Events;
using MediatR;

namespace CarTracker.Domain.Commands
{
    public class ChangeOilCommand : IRequest<DomainEvent[]>
    {
        public string ID { get; set; }

        public int Mileage { get; set; }

        public decimal Charge { get; set; }
    }
}
