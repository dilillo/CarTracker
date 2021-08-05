using CarTracker.Domain.Aggregates;
using CarTracker.Domain.Events;
using CarTracker.Domain.Projectors;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarTracker.Domain.Commands
{
    public class NewOwnerCommandHandler : IRequestHandler<NewOwnerCommand, DomainEvent[]>
    {
        private readonly ICarAggregate _carAggregate;
        private readonly IGetCarByIDViewProjector _getCarByIDViewProjector;

        public NewOwnerCommandHandler(ICarAggregate carAggregate, IGetCarByIDViewProjector getCarByIDViewProjector)
        {
            _carAggregate = carAggregate;
            _getCarByIDViewProjector = getCarByIDViewProjector;
        }

        public async Task<DomainEvent[]> Handle(NewOwnerCommand request, CancellationToken cancellationToken)
        {
            await _carAggregate.Load(request.ID, cancellationToken);

            _carAggregate.NewOwner(request.NewOwner);

            var events = await _carAggregate.SaveChanges(cancellationToken);

            await _getCarByIDViewProjector.Project(events, cancellationToken);

            return events;
        }
    }
}
