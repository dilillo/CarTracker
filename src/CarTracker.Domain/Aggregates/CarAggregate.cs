using CarTracker.Domain.Events;
using CarTracker.Domain.Exceptions;
using CarTracker.Domain.Repositories;
using System;

namespace CarTracker.Domain.Aggregates
{
    public interface ICarAggregate : IAggregate
    {
        void AddCar(string make, string model, string owner);

        void ChangeOil(int mileage, decimal charge);

        void InspectBrakes(int mileage, double remainingPad);

        void NewOwner(string newOwner);

        void ReplaceTires(int numberOfTiresReplaced, decimal charge);
    }

    public class CarAggregate : Aggregate, ICarAggregate
    {
        public CarAggregate(IDomainEventRepository domainEventRepository) : base(domainEventRepository)
        {
        }

        private string _owner;

        public void AddCar(string make, string model, string owner)
        {
            ValidateNotAlreadyAdded();

            var @event = DomainEventFactory.Build<CarAddedEvent>(AggregateID, Version + 1, i =>
            {
                i.Make = make;
                i.Model = model;
                i.Owner = owner;
            });

            Mutate(@event);

            AddPendingChange(@event);
        }

        public void ChangeOil(int mileage, decimal charge)
        {
            ValidateAlreadyAdded();

            var @event = DomainEventFactory.Build<OilChangedEvent>(AggregateID, Version + 1, i =>
            {
                i.Mileage = mileage;
                i.Charge = charge;
            });

            Mutate(@event);

            AddPendingChange(@event);
        }

        public void InspectBrakes(int mileage, double remainingPad)
        {
            ValidateAlreadyAdded();

            var @event = DomainEventFactory.Build<BrakesInspectedEvent>(AggregateID, Version + 1, i =>
            {
                i.Mileage = mileage;
                i.RemainingPad = remainingPad;
            });

            Mutate(@event);

            AddPendingChange(@event);
        }

        public void NewOwner(string newOwner)
        {
            ValidateAlreadyAdded();

            ValidateOwnershipChange(newOwner);

            var @event = DomainEventFactory.Build<NewOwnerEvent>(AggregateID, Version + 1, i =>
            {
                i.NewOwner = newOwner;
            });

            Mutate(@event);

            AddPendingChange(@event);
        }

        public void ReplaceTires(int numberOfTiresReplaced, decimal charge)
        {
            ValidateAlreadyAdded();

            var @event = DomainEventFactory.Build<TiresReplacedEvent>(AggregateID, Version + 1, i =>
            {
                i.NumberOfTiresReplaced = numberOfTiresReplaced;
                i.Charge = charge;
            });

            Mutate(@event);

            AddPendingChange(@event);
        }

        private void ValidateAlreadyAdded()
        {
            if (string.IsNullOrEmpty(_owner))
            {
                throw new CarMustBeAddedFirstException();
            }
        }

        private void ValidateNotAlreadyAdded()
        {
            if (!string.IsNullOrEmpty(_owner))
            {
                throw new CarAlreadyAddedException();
            }
        }

        private void ValidateOwnershipChange(string newOwner)
        {
            if (_owner.Equals(newOwner, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new OwnershipChangeException();
            }
        }

        protected override void Mutate(DomainEvent @event)
        {
            base.Mutate(@event);

            switch (@event)
            {
                case CarAddedEvent carAddedEvent:

                    _owner = carAddedEvent.Owner;

                    break;

                case NewOwnerEvent newOwnerEvent:

                    _owner = newOwnerEvent.NewOwner;

                    break;
            }
        }
    }
}
