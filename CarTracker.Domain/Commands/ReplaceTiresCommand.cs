using CarTracker.Domain.Events;
using MediatR;

namespace CarTracker.Domain.Commands
{
    public class ReplaceTiresCommand : IRequest<DomainEvent[]>
    {
        public string ID { get; set; }

        public int NumberOfTiresReplaced { get; set; }

        public decimal Charge { get; set; }
    }
}
