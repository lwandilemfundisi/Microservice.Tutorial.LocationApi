using Microservice.Framework.Domain.Queries;
using Microservice.Framework.Persistence;
using Microservice.Framework.Persistence.EFCore.Queries.CriteriaQueries;
using Microservice.Framework.Persistence.EFCore.Queries.Filtering;
using System.Threading;
using System.Threading.Tasks;

namespace LocationApi.Domain.DomainModel.LocationModel.Queries
{
    public class GetLocationQuery : EFCoreCriteriaDomainQuery<LocationAggregate>, IQuery<LocationAggregate>
    {
        protected override void OnBuildDomainCriteria(EFCoreDomainCriteria domainCriteria)
        {
            base.OnBuildDomainCriteria(domainCriteria);
        }
    }

    public class GetLocationQueryHandler : EFCoreCriteriaDomainQueryHandler<LocationAggregate>, IQueryHandler<GetLocationQuery, LocationAggregate>
    {
        public GetLocationQueryHandler(IPersistenceFactory persistenceFactory)
            : base(persistenceFactory)
        {
        }

        public async Task<LocationAggregate> ExecuteQueryAsync(GetLocationQuery query, CancellationToken cancellationToken)
        {
            return await Find(query);
        }
    }
}
