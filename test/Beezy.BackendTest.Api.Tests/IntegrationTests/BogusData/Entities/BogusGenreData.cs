using System.Collections.Generic;
using System.Linq;
using Beezy.BackendTest.Domain.Entities;
using Bogus;

namespace Beezy.BackendTest.Api.Tests.IntegrationTests.BogusData.Entities
{
    public class BogusGenreData
    {
        public static IList<Genre> GetGenreList(int count) =>
            new Faker<Genre>()
                .RuleFor(g => g.Id, f => f.UniqueIndex)
                .RuleFor(g => g.Name, f => f.Lorem.Word())
                .Generate(count)
                .ToList();
    }
}
