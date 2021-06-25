using LocationApi.Persistence.Mappings.LocationModel;
using Microsoft.EntityFrameworkCore;

namespace LocationApi.Persistence
{
    public class LocationDbContext : DbContext
    {
        public LocationDbContext(DbContextOptions<LocationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .LocationModelMap();
        }
    }
}
