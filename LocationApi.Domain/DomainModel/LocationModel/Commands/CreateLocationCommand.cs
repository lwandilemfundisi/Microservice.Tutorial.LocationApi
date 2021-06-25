using Microservice.Framework.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocationApi.Domain.DomainModel.LocationModel.Commands
{
    public class CreateLocationCommand : Command<LocationAggregate, LocationAggregateId>
    {
        public CreateLocationCommand(
            LocationAggregateId aggregateId,
            string locationName)
            : base(aggregateId)
        {
            LocationName = locationName;
        }

        public string LocationName { get; }
    }

    public class CreateLocationCommandHandler : CommandHandler<LocationAggregate, LocationAggregateId, CreateLocationCommand>
    {
        public override Task ExecuteAsync(LocationAggregate aggregate, CreateLocationCommand command, CancellationToken cancellationToken)
        {
            aggregate.Create(command.LocationName);
            return Task.FromResult(0);
        }
    }
}
