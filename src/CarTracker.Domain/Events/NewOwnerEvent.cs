namespace CarTracker.Domain.Events
{
    public class NewOwnerEvent : DomainEvent
    {
        public string NewOwner { get; set; }
    }
}
