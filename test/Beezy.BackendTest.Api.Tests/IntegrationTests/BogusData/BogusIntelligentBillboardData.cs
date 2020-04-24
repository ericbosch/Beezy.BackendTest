using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beezy.BackendTest.Domain.Entities;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models;
using Bogus;

namespace Beezy.BackendTest.Api.Tests.IntegrationTests.BogusData
{
    public class BogusIntelligentBillboardData
    {
        public static IList<GetIntelligentBillboardRequest> GetIntelligentBillboardRequestsList(int count) =>
            new Faker<GetIntelligentBillboardRequest>()
                .RuleFor(r => r.TimePeriod, f => f.Random.Int(1, 10))
                .RuleFor(r => r.BigRooms, f => f.Random.Int(1, 10))
                .RuleFor(r => r.SmallRooms, f => f.Random.Int(1, 10))
                .Generate(count)
                .ToList();
    }
}
