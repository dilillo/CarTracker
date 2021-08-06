using CarTracker.Domain.Repositories;
using System;

namespace CarTracker.Projector.Repositories
{
    public class ProjectorDomainViewRepository : DomainViewRepository
    {
        public ProjectorDomainViewRepository() : base()
        {
            _connectionString = Environment.GetEnvironmentVariable("CarTrackerOptions:CosmosDBConnectionString");
        }
    }
}
