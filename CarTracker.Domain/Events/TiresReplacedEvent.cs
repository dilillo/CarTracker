namespace CarTracker.Domain.Events
{
    public class TiresReplacedEvent : DomainEvent
    {
        public int NumberOfTiresReplaced { get; set; }

        public decimal Charge { get; set; }
    }
}
