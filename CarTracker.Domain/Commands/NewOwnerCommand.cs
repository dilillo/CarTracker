using CarTracker.Domain.Events;
using MediatR;

namespace CarTracker.Domain.Commands
{
    public class NewOwnerCommand : IRequest<DomainEvent[]>
    {
        public string ID { get; set; }

        public string NewOwner { get; set; }
    }
}
