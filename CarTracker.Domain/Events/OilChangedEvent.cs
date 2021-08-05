namespace CarTracker.Domain.Events
{
    public class OilChangedEvent : DomainEvent
    {
        public int Mileage { get; set; }

        public decimal Charge { get; set; }
    }
}
