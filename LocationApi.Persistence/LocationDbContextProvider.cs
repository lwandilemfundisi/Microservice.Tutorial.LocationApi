using Microservice.Framework.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace LocationApi.Persistence
{
    public class LocationDbContextProvider : IDbContextProvider<LocationDbContext>, IDisposable
    {
        private readonly DbContextOptions<LocationDbContext> _options;

        public LocationDbContextProvider(IConfiguration configuration)
        {
            _options = new DbContextOptionsBuilder<LocationDbContext>()
                .UseSqlServer(configuration["DataConnection:Database"])
                .Options;
        }

        public LocationDbContext CreateContext()
        {
            return new LocationDbContext(_options);
        }

        public void Dispose()
        {
        }
    }
}
