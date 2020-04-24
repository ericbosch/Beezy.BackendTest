using System.Collections.Generic;
using System.Linq;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard;
using Bogus;

namespace Beezy.BackendTest.Api.Tests.IntegrationTests.BogusData.Models
{
    public class BogusIntelligentBillboardData
    {
        public static IList<GetIntelligentBillboardRequest> GetIntelligentBillboardRequestsList(int count) =>
            new Faker<GetIntelligentBillboardRequest>()
                .RuleFor(r => r.TimePeriod, f => f.Random.Int(1, count))
                .RuleFor(r => r.BigRooms, f => f.Random.Int(1, count))
                .RuleFor(r => r.SmallRooms, f => f.Random.Int(1, count))
                .RuleFor(r => r.City, f => f.Address.City())
                .Generate(count)
                .ToList();
    }
}
