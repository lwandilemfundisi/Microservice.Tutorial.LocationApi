using Microservice.Framework.Domain;
using Microservice.Framework.Domain.Extensions;
using System.Reflection;

namespace LocationApi.Domain
{
    public static class LocationDomainExtensions
    {
        public static Assembly Assembly { get; } = typeof(LocationDomainExtensions).Assembly;

        public static IDomainContainer ConfigureLocationDomain(this IDomainContainer domainContainer)
        {
            return domainContainer
                .AddDefaults(Assembly);
        }
    }
}
