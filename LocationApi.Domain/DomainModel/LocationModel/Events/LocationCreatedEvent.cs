using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Events.AggregateEvents;

namespace LocationApi.Domain.DomainModel.LocationModel.Events
{
    [EventVersion("LocationCreated", 1)]
    public class LocationCreatedEvent : AggregateEvent<LocationAggregate, LocationAggregateId>
    {
        public LocationCreatedEvent(
            string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
