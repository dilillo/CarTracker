using CarTracker.Domain.Aggregates;
using CarTracker.Domain.Events;
using CarTracker.Domain.Projectors;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarTracker.Domain.Commands
{
    public class ReplaceTiresCommandHandler : IRequestHandler<ReplaceTiresCommand, DomainEvent[]>
    {
        private readonly ICarAggregate _carAggregate;
        private readonly IGetCarByIDViewProjector _getCarByIDViewProjector;

        public ReplaceTiresCommandHandler(ICarAggregate carAggregate, IGetCarByIDViewProjector getCarByIDViewProjector)
        {
            _carAggregate = carAggregate;
            _getCarByIDViewProjector = getCarByIDViewProjector;
        }

        public async Task<DomainEvent[]> Handle(ReplaceTiresCommand request, CancellationToken cancellationToken)
        {
            await _carAggregate.Load(request.ID, cancellationToken);

            _carAggregate.ReplaceTires(request.NumberOfTiresReplaced, request.Charge);

            var events = await _carAggregate.SaveChanges(cancellationToken);

            await _getCarByIDViewProjector.Project(events, cancellationToken);

            return events;
        }
    }
}
