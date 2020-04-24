using System.Collections.Generic;
using System.Linq;
using Beezy.BackendTest.Domain.Entities;
using Bogus;

namespace Beezy.BackendTest.Api.Tests.IntegrationTests.BogusData.Entities
{
    public class BogusCityData
    {
        public static City GetCity(string city)
        {
            return new Faker<City>()
                .RuleFor(c => c.Id, f => f.UniqueIndex)
                .RuleFor(c => c.Name, f => city)
                .RuleFor(c => c.Population, f => f.Random.Int(1));
        }
    }
}
