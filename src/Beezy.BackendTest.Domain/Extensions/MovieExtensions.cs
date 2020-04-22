using System;
using System.Collections.Generic;
using System.Linq;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models;

namespace Beezy.BackendTest.Domain.Extensions
{
    public static class MovieExtensions
    {
        public static IEnumerable<Movie> WithScreenSize(this IEnumerable<Movie> movies, ScreenSize screenSize)
        {
            return movies.Where(m => m.ScreenSize == screenSize);
        }

        public static IEnumerable<Movie> ThatCanBeReleasedAtDate(this IEnumerable<Movie> movies, DateTime startDate)
        {
            return movies.Where(m => m.ReleaseDate <= startDate);
        }
    }
}
