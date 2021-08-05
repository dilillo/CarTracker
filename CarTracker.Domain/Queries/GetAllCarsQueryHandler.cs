using CarTracker.Domain.Projectors;
using CarTracker.Domain.Repositories;
using CarTracker.Domain.Views;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarTracker.Domain.Queries
{
    public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, GetAllCarsView>
    {
        private readonly IDomainViewRepository _domainViewRepository;
        private readonly IDomainEventRepository _domainEventRepository;
        private readonly IGetAllCarsViewProjector _personGetAllViewProjector;

        public GetAllCarsQueryHandler(IDomainViewRepository domainViewRepository, IDomainEventRepository domainEventRepository, IGetAllCarsViewProjector personGetAllViewProjector)
        {
            _domainViewRepository = domainViewRepository;
            _personGetAllViewProjector = personGetAllViewProjector;
            _domainEventRepository = domainEventRepository;
        }

        public async Task<GetAllCarsView> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        {
            var data = await _domainViewRepository.GetByID(DomainViewIDs.GetAllCarsView, cancellationToken) as GetAllCarsView ?? new GetAllCarsView();

            var unappliedEvents = await _domainEventRepository.GetMoreRecentThan(data.AggregateVersions, cancellationToken);

            if (unappliedEvents.Count > 0)
            {
                data = _personGetAllViewProjector.Predict(data, unappliedEvents.ToArray());
            }

            return data;
        }
    }
}
