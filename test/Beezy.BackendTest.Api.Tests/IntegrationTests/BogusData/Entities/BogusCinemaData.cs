using System.Collections.Generic;
using System.Linq;
using Beezy.BackendTest.Domain.Entities;
using Bogus;

namespace Beezy.BackendTest.Api.Tests.IntegrationTests.BogusData.Entities
{
    public class BogusCinemaData
    {
        public static IList<Cinema> GetCinemaList(int count) =>
            new Faker<Cinema>()
                .RuleFor(c => c.Id, f => f.UniqueIndex)
                .RuleFor(c => c.Name, f => f.Lorem.Word())
                .RuleFor(c => c.OpenSince, f => f.Date.Recent())
                .Generate(count)
                .ToList();
    }
}
