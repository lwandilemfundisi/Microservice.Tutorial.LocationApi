using Microservice.Framework.Common;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

namespace LocationApi.Domain.DomainModel.LocationModel
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class LocationAggregateId : SingleValueObject<string>, IIdentity
    {
        private static readonly Regex ValidValues = new Regex("[a-zA-Z]{2}[a-zA-Z2-9]{3}", RegexOptions.Compiled);

        public LocationAggregateId(string value) : base(value)
        {
            if (!ValidValues.IsMatch(value)) throw new ArgumentException($"'{value} is not a valid UN location code'");
        }
    }
}
