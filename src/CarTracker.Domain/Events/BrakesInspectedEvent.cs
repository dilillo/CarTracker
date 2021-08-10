namespace CarTracker.Domain.Events
{
    public class BrakesInspectedEvent : DomainEvent
    {
        public int Mileage { get; set; }

        public double RemainingPad { get; set; }
    }
}
