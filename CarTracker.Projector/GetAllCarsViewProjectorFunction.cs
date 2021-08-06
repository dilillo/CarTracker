using CarTracker.Domain.Events;
using CarTracker.Domain.Projectors;
using CarTracker.Projector.Repositories;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarTracker.Projector
{
    public static class GetAllCarsViewProjectorFunction
    {
        [FunctionName("GetAllCarsViewProjectorFunction")]
        public static async Task Run([CosmosDBTrigger(
            databaseName: "cartracker",
            collectionName: "events",
            ConnectionStringSetting = "CarTrackerOptions:CosmosDBConnectionString",
            LeaseCollectionName = "leases",
            LeaseCollectionPrefix = "GetAllCarsViewProjectorFunction",
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> input, ILogger log, CancellationToken cancellationToken)
        {
            var events = input.Select(i => JsonConvert.DeserializeObject<DomainEventWrapper>(i.ToString()).GetEvent()).ToArray();

            var projector = new GetAllCarsViewProjector(new ProjectorDomainViewRepository());

            await projector.Project(events, cancellationToken);

            log.LogInformation($"{nameof(GetAllCarsViewProjectorFunction)} processed {input.Count} documents");
        }
    }
}
