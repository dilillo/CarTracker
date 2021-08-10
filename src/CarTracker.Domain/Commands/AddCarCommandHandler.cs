using CarTracker.Domain.Aggregates;
using CarTracker.Domain.Events;
using CarTracker.Domain.Projectors;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarTracker.Domain.Commands
{
    public class AddCarCommandHandler : IRequestHandler<AddCarCommand, DomainEvent[]>
    {
        private readonly ICarAggregate _carAggregate;
        private readonly IGetCarByIDViewProjector _getCarByIDViewProjector;

        public AddCarCommandHandler(ICarAggregate carAggregate, IGetCarByIDViewProjector getCarByIDViewProjector)
        {
            _carAggregate = carAggregate;
            _getCarByIDViewProjector = getCarByIDViewProjector;
        }

        public async Task<DomainEvent[]> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            await _carAggregate.Load(request.ID, cancellationToken);

            _carAggregate.AddCar(request.Make, request.Model, request.Owner);

            var events = await _carAggregate.SaveChanges(cancellationToken);

            await _getCarByIDViewProjector.Project(events, cancellationToken);

            return events;
        }
    }
}
