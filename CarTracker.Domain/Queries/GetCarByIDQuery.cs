using CarTracker.Domain.Events;
using CarTracker.Domain.Views;
using MediatR;
using System.Collections.Generic;

namespace CarTracker.Domain.Queries
{
    public class GetCarByIDQuery : IRequest<GetCarByIDQueryResult>
    {
        public string ID { get; set; }
    }

    public class GetCarByIDQueryResult
    {
        public GetCarByIDView GetCarByIDView { get; set; }

        public List<DomainEvent> Events { get; set; }
    }
}
