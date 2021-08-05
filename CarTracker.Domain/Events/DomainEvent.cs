using MediatR;
using System;

namespace CarTracker.Domain.Events
{
    public class DomainEvent : INotification
    {
        public string ID { get; set; }

        public string AggregateID { get; set; }

        public int Version { get; set; }

        public DateTimeOffset FiredAtDateTimeUtc { get; set; }
    }
}
