using System.Collections.Generic;
using System.Linq;
using Beezy.BackendTest.Domain.Entities;
using Bogus;

namespace Beezy.BackendTest.Api.Tests.IntegrationTests.BogusData.Entities
{
    public class BogusRoomData
    {
        private static readonly string[] Sizes = {"Big", "Small"};

        public static IList<Room> GetRoomList(int count) =>
            new Faker<Room>()
                .RuleFor(r => r.Id, f => f.UniqueIndex)
                .RuleFor(r => r.Name, f => f.Lorem.Word())
                .RuleFor(r => r.Size, f => f.PickRandom(Sizes))
                .RuleFor(r => r.Seats, f => f.Random.Int(1, 100))
                .Generate(count)
                .ToList();
    }
}
