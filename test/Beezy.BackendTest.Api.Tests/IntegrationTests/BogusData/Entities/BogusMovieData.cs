using System.Collections.Generic;
using System.Linq;
using Beezy.BackendTest.Domain.Entities;
using Bogus;

namespace Beezy.BackendTest.Api.Tests.IntegrationTests.BogusData.Entities
{
    public class BogusMovieData
    {
        public static IList<Movie> GetMovieList(int count) =>
            new Faker<Movie>()
                .RuleFor(m => m.Id, f => f.UniqueIndex)
                .RuleFor(m => m.OriginalTitle, f => f.Lorem.Text())
                .RuleFor(m => m.ReleaseDate, f => f.Date.Past())
                .RuleFor(m => m.OriginalLanguage, f => f.Random.RandomLocale())
                .Generate(count)
                .ToList();
    }
}
