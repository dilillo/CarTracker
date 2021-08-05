using CarTracker.Domain.Events;
using CarTracker.Domain.Repositories;
using CarTracker.Domain.Views;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarTracker.Domain.Projectors
{
    public interface IGetCarByIDViewProjector
    {
        GetCarByIDView Predict(GetCarByIDView domainView, DomainEvent[] domainEvents);

        Task Project(DomainEvent[] domainEvents, CancellationToken cancellationToken);
    }

    public class GetCarByIDViewProjector : DomainViewProjector<GetCarByIDView>, IGetCarByIDViewProjector
    {
        private readonly IDomainViewRepository _domainViewRepository;

        public GetCarByIDViewProjector(IDomainViewRepository domainViewRepository)
        {
            _domainViewRepository = domainViewRepository;
        }

        protected override Type[] GetProjectedDomainEventTypes()
        {
            return new Type[]
            {
                typeof(CarAddedEvent),
                typeof(BrakesInspectedEvent),
                typeof(NewOwnerEvent),
                typeof(OilChangedEvent),
                typeof(TiresReplacedEvent)
            };
        }

        public GetCarByIDView Predict(GetCarByIDView domainView, DomainEvent[] domainEvents)
        {
            var projectableDomainEvents = GetProjectableDomainEvents(domainEvents);

            if (projectableDomainEvents.Count > 0)
            {
                ApplyDomainEvents(projectableDomainEvents, domainView);
            }

            return domainView;
        }

        public async Task Project(DomainEvent[] domainEvents, CancellationToken cancellationToken)
        {
            var projectableDomainEvents = GetProjectableDomainEvents(domainEvents);

            if (projectableDomainEvents.Count == 0)
            {
                return;
            }

            var aggregateEventGroups = domainEvents.GroupBy(i => i.AggregateID);

            foreach (var aggregateEventGroup in aggregateEventGroups)
            {
                var domainView = await GetDomainView(aggregateEventGroup.Key, cancellationToken);

                ApplyDomainEvents(projectableDomainEvents, domainView);

                await _domainViewRepository.Save(domainView, cancellationToken);
            }
        }

        protected override void ApplyDomainEvent(GetCarByIDView domainView, DomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case CarAddedEvent carAddedEvent:

                    domainView.Car = new GetCarByIDViewCar
                    {
                        ID = carAddedEvent.AggregateID,
                        Make = carAddedEvent.Make,
                        Model = carAddedEvent.Model,
                        Owner = carAddedEvent.Owner
                    };

                    break;

                case BrakesInspectedEvent brakesInspectedEvent:

                    domainView.Car.Mileage = brakesInspectedEvent.Mileage;
                    domainView.Car.RemainingPad = brakesInspectedEvent.RemainingPad;

                    break;

                case NewOwnerEvent newOwnerEvent:

                    domainView.Car.Owner = newOwnerEvent.NewOwner;

                    break;

                case OilChangedEvent oilChangedEvent:

                    domainView.Car.Mileage = oilChangedEvent.Mileage;
                    domainView.Car.LastOilChange = oilChangedEvent.FiredAtDateTimeUtc;
                    domainView.Car.TotalCharges += oilChangedEvent.Charge;

                    break;

                case TiresReplacedEvent tiresReplacedEvent:

                    domainView.Car.TotalCharges += tiresReplacedEvent.Charge;

                    break;
            }
        }

        private async Task<GetCarByIDView> GetDomainView(string aggregateID, CancellationToken cancellationToken)
        {
            var doimainView = (await _domainViewRepository.GetByID(DomainViewIDs.GetCarByIDView(aggregateID), cancellationToken)) as GetCarByIDView;

            return doimainView ?? new GetCarByIDView(aggregateID);
        }
    }
}
