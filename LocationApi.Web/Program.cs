using LocationApi.Domain.DomainModel.LocationModel;
using LocationApi.Persistence;
using Microservice.Framework.Common;
using Microservice.Framework.Domain.Aggregates;
using Microservice.Framework.Persistence.EFCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace LocationApi.Web
{
    public class Program
    {
        private static IAggregateStore _aggregateStore;

        public async static Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                // uncomment to write to Azure diagnostics stream
                //.WriteTo.File(
                //    @"D:\home\LogFiles\Application\identityserver.txt",
                //    fileSizeLimitBytes: 1_000_000,
                //    rollOnFileSizeLimit: true,
                //    shared: true,
                //    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                _aggregateStore = scope.ServiceProvider.GetRequiredService<IAggregateStore>();

                var db = scope.ServiceProvider.GetRequiredService<IDbContextProvider<LocationDbContext>>();
                db.CreateContext().Database.Migrate();

                //To add default sample data
                await Task.WhenAll(Locations.GetLocations().Select(CreateLocationAggregateAsync));
            }

            host.Run();
        }

        public static Task CreateLocationAggregateAsync(LocationAggregate location)
        {
            return UpdateAsync<LocationAggregate, LocationAggregateId>(location.Id, a => a.Create(location.LocationName));
        }

        private async static Task UpdateAsync<TAggregate, TIdentity>(TIdentity id, Action<TAggregate> action)
            where TAggregate : class, IAggregateRoot<TIdentity>
            where TIdentity : IIdentity
        {
            await _aggregateStore.UpdateAsync<TAggregate, TIdentity>(
                id,
                SourceId.New,
                (a, c) =>
                {
                    action(a);
                    return Task.FromResult(0);
                },
                CancellationToken.None);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public static class Locations
    {
        public static readonly LocationAggregateId Hongkong = new LocationAggregateId("CNHKG");
        public static readonly LocationAggregateId Melbourne = new LocationAggregateId("AUMEL");
        public static readonly LocationAggregateId Stockholm = new LocationAggregateId("SESTO");
        public static readonly LocationAggregateId Helsinki = new LocationAggregateId("FIHEL");
        public static readonly LocationAggregateId Chicago = new LocationAggregateId("USCHI");
        public static readonly LocationAggregateId Tokyo = new LocationAggregateId("JNTKO");
        public static readonly LocationAggregateId Hamburg = new LocationAggregateId("DEHAM");
        public static readonly LocationAggregateId Shanghai = new LocationAggregateId("CNSHA");
        public static readonly LocationAggregateId Rotterdam = new LocationAggregateId("NLRTM");
        public static readonly LocationAggregateId Gothenburg = new LocationAggregateId("SEGOT");
        public static readonly LocationAggregateId Hangzou = new LocationAggregateId("CNHGH");
        public static readonly LocationAggregateId NewYork = new LocationAggregateId("USNYC");
        public static readonly LocationAggregateId Dallas = new LocationAggregateId("USDAL");

        public static IEnumerable<LocationAggregate> GetLocations()
        {
            var fieldInfos = typeof(Locations).GetFields(BindingFlags.Public | BindingFlags.Static);
            return fieldInfos.Select(fi => new LocationAggregate((LocationAggregateId)fi.GetValue(null)) { LocationName = fi.Name });
        }
    }
}
