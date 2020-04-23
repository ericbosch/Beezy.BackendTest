using System;
using System.Collections.Generic;
using System.Linq;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models;

namespace Beezy.BackendTest.Domain.Extensions
{
    public static class MovieExtensions
    {
        public static IEnumerable<MovieInfo> WithScreenSize(this IEnumerable<MovieInfo> movies, ScreenSize screenSize)
        {
            return movies.Where(m => m.ScreenSize == screenSize);
        }

        public static IEnumerable<MovieInfo> ThatCanBeReleasedAtDate(this IEnumerable<MovieInfo> movies, DateTime startDate)
        {
            return movies.Where(m => m.ReleaseDate <= startDate);
        }
    }
}
