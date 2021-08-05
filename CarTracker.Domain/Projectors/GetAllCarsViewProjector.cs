using CarTracker.Domain.Events;
using CarTracker.Domain.Repositories;
using CarTracker.Domain.Views;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarTracker.Domain.Projectors
{
    public interface IGetAllCarsViewProjector
    {
        GetAllCarsView Predict(GetAllCarsView domainView, DomainEvent[] domainEvents);

        Task Project(DomainEvent[] domainEvents, CancellationToken cancellationToken);
    }

    public class GetAllCarsViewProjector : DomainViewProjector<GetAllCarsView>, IGetAllCarsViewProjector
    {
        private readonly IDomainViewRepository _domainViewRepository;

        public GetAllCarsViewProjector(IDomainViewRepository domainViewRepository)
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

        public GetAllCarsView Predict(GetAllCarsView domainView, DomainEvent[] domainEvents)
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

            var domainView = await GetDomainView(cancellationToken);

            ApplyDomainEvents(projectableDomainEvents, domainView);

            await _domainViewRepository.Save(domainView, cancellationToken);
        }

        protected override void ApplyDomainEvent(GetAllCarsView domainView, DomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case CarAddedEvent carAddedEvent:

                    ApplyCarAddedEvent(domainView, carAddedEvent);

                    break;

                case NewOwnerEvent newOwnerEvent:

                    ApplyNewOwnerEvent(domainView, newOwnerEvent);

                    break;

                default:

                    ApplyOtherServiceEvent(domainView, domainEvent);

                    break;
            }
        }

        private static void ApplyOtherServiceEvent(GetAllCarsView domainView, DomainEvent domainEvent)
        {
            var existingCarToUpdate = domainView.Cars.FirstOrDefault(i => i.ID == domainEvent.AggregateID);

            if (existingCarToUpdate != null)
            {
                existingCarToUpdate.LastServiced = domainEvent.FiredAtDateTimeUtc;
            }
        }

        private static void ApplyNewOwnerEvent(GetAllCarsView domainView, NewOwnerEvent newOwnerEvent)
        {
            var existingCarToUpdate = domainView.Cars.FirstOrDefault(i => i.ID == newOwnerEvent.AggregateID);

            if (existingCarToUpdate != null)
            {
                existingCarToUpdate.Owner = newOwnerEvent.NewOwner;
            }
        }

        private static void ApplyCarAddedEvent(GetAllCarsView domainView, CarAddedEvent carAddedEvent)
        {
            domainView.Cars.Add(new GetAllCarsViewCar
            {
                ID = carAddedEvent.AggregateID,
                Make = carAddedEvent.Make,
                Model = carAddedEvent.Model,
                Owner = carAddedEvent.Owner
            });
        }

        private async Task<GetAllCarsView> GetDomainView(CancellationToken cancellationToken)
        {
            var domainView = (await _domainViewRepository.GetByID(DomainViewIDs.GetAllCarsView, cancellationToken)) as GetAllCarsView;

            return domainView ?? new GetAllCarsView();
        }
    }
}
