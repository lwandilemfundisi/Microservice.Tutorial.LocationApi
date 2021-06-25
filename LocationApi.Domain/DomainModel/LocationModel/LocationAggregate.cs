using LocationApi.Domain.DomainModel.LocationModel.Events;
using Microservice.Framework.Domain.Aggregates;
using Microservice.Framework.Domain.Exceptions;

namespace LocationApi.Domain.DomainModel.LocationModel
{
    public class LocationAggregate : AggregateRoot<LocationAggregate, LocationAggregateId>
    {
        #region Constructors

        public LocationAggregate()
            : this(null)
        {

        }

        public LocationAggregate(LocationAggregateId aggregateId)
            : base(aggregateId)
        {

        }

        #endregion

        #region Properties

        public string LocationName { get; set; }

        #endregion

        #region Methods

        public void Create(string name)
        {
            if (!IsNew) throw DomainError.With("Location is already created");
            LocationName = name;
            Emit(new LocationCreatedEvent(name));
        }

        #endregion
    }
}
