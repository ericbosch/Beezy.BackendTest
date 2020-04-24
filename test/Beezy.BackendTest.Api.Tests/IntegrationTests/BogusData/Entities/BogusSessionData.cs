using System;
using System.Collections.Generic;
using System.Linq;
using Beezy.BackendTest.Domain.Entities;
using Bogus;

namespace Beezy.BackendTest.Api.Tests.IntegrationTests.BogusData.Entities
{
    public class BogusSessionData
    {
        public static IList<Session> GetSessionList(int count, DateTime now)
        {
            string contextLocale = new Faker().Locale;
            DateTime randomStartTime = new Faker(contextLocale).Date.Between(now.Date, now.AddHours(24));
            DateTime randomEndTime = new Faker(contextLocale).Date.Between(randomStartTime, now.AddHours(24));

            return new Faker<Session>()
                .RuleFor(s => s.Id, f => f.UniqueIndex)
                .RuleFor(s => s.StartTime, f => randomStartTime)
                .RuleFor(s => s.EndTime, f => randomEndTime)
                .RuleFor(s => s.SeatsSold, f => f.Random.Int(1, 100))
                .Generate(count)
                .ToList();
        }
    }
}
