using CarTracker.Domain.Repositories;
using CarTracker.Domain.Views;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarTracker.Domain.Queries
{
    public class GetCarByIDQueryHandler : IRequestHandler<GetCarByIDQuery, GetCarByIDQueryResult>
    {
        private readonly IDomainViewRepository _domainViewRepository;
        private readonly IDomainEventRepository _domainEventRepository;

        public GetCarByIDQueryHandler(IDomainViewRepository domainViewRepository, IDomainEventRepository domainEventRepository)
        {
            _domainViewRepository = domainViewRepository;
            _domainEventRepository = domainEventRepository;
        }

        public async Task<GetCarByIDQueryResult> Handle(GetCarByIDQuery request, CancellationToken cancellationToken)
        {
            var data = await _domainViewRepository.GetByID(DomainViewIDs.GetCarByIDView(request.ID), cancellationToken);
            var events = await _domainEventRepository.GetByAggregateID(request.ID, cancellationToken);

            return new GetCarByIDQueryResult
            {
                GetCarByIDView = data as GetCarByIDView ?? new GetCarByIDView(request.ID),
                Events = events
            };
        }
    }
}
