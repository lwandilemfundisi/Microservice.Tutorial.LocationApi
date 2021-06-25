using Microservice.Framework.Domain.Queries;
using Microservice.Framework.Persistence;
using Microservice.Framework.Persistence.EFCore.Queries.CriteriaQueries;
using Microservice.Framework.Persistence.EFCore.Queries.Filtering;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LocationApi.Domain.DomainModel.LocationModel.Queries
{
    public class GetLocationsQuery : EFCoreCriteriaDomainQuery<LocationAggregate>, IQuery<IReadOnlyCollection<LocationAggregate>>
    {
        protected override void OnBuildDomainCriteria(EFCoreDomainCriteria domainCriteria)
        {
            base.OnBuildDomainCriteria(domainCriteria);
        }
    }

    public class GetLocationsQueryHandler : EFCoreCriteriaDomainQueryHandler<LocationAggregate>, IQueryHandler<GetLocationsQuery, IReadOnlyCollection<LocationAggregate>>
    {
        public GetLocationsQueryHandler(IPersistenceFactory persistenceFactory)
            : base(persistenceFactory)
        {
        }

        public async Task<IReadOnlyCollection<LocationAggregate>> ExecuteQueryAsync(GetLocationsQuery query, CancellationToken cancellationToken)
        {
            return (IReadOnlyCollection<LocationAggregate>)await FindAll(query);
        }
    }
}
