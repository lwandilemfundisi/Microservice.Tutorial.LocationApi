using LocationApi.Domain.DomainModel.LocationModel;
using Microsoft.EntityFrameworkCore;

namespace LocationApi.Persistence.Mappings.LocationModel
{
    public static class LocationModelMapping
    {
        public static ModelBuilder LocationModelMap(this ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<LocationAggregate>()
            .Property(o => o.Id)
            .HasConversion(new SingleValueObjectIdentityValueConverter<LocationAggregateId>());

            return modelBuilder;
        }
    }
}
