using System.Collections.Generic;
using System.Linq;
using Beezy.BackendTest.Domain.Entities;
using Bogus;

namespace Beezy.BackendTest.Api.Tests.IntegrationTests.BogusData.Entities
{
    public class BogusMovieGenreData
    {
        public static IList<MovieGenre> GetMovieGenreList(int count) =>
            new Faker<MovieGenre>()
                .Generate(count)
                .ToList();
    }
}
