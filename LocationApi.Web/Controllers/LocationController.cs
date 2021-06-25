using LocationApi.Domain.DomainModel.LocationModel;
using LocationApi.Domain.DomainModel.LocationModel.Commands;
using LocationApi.Domain.DomainModel.LocationModel.Queries;
using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.ExecutionResults;
using Microservice.Framework.Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LocationApi.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandBus _commandBus;

        public LocationController(
            IQueryProcessor queryProcessor,
            ICommandBus commandBus
            )
        {
            _queryProcessor = queryProcessor;
            _commandBus = commandBus;
        }

        [HttpGet("getlocations")]
        public async Task<IReadOnlyCollection<LocationAggregate>> GetAll()
        {
            return await _queryProcessor.ProcessAsync(new GetLocationsQuery(), CancellationToken.None);
        }

        [HttpGet("getlocation")]
        public async Task<LocationAggregate> Get()
        {
            return await _queryProcessor.ProcessAsync(new GetLocationQuery(), CancellationToken.None);
        }

        [HttpPost("createlocation")]
        public async Task<IExecutionResult> Post(string locationId, string locationName)
        {
            return await _commandBus.PublishAsync(
                new CreateLocationCommand(
                    new LocationAggregateId(locationId),
                    locationName), CancellationToken.None);
        }
    }
}
