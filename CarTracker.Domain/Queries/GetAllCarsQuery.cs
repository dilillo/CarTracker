using CarTracker.Domain.Events;
using CarTracker.Domain.Views;
using MediatR;

namespace CarTracker.Domain.Queries
{
    public class GetAllCarsQuery : IRequest<GetAllCarsView>
    {
        public DomainEvent[] PendingDomainEvents { get; set; }
    }
}
