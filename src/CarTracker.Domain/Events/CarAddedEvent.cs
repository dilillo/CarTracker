namespace CarTracker.Domain.Events
{
    public class CarAddedEvent : DomainEvent
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public string Owner { get; set; }
    }
}
