using System;
using System.Collections.Generic;

namespace Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models
{
    public class Billboard
    {

        public DateTime StartDate { get; }
        public List<MovieInfo> BigScreenMovies { get; }
        public List<MovieInfo> SmallScreenMovies { get; }

        private Billboard(DateTime startDate, List<MovieInfo> bigScreenMovies, List<MovieInfo> smallScreenMovies)
        {
            StartDate = startDate;
            BigScreenMovies = bigScreenMovies;
            SmallScreenMovies = smallScreenMovies;
        }
        public static Billboard Create(DateTime startDate, List<MovieInfo> bigScreenMovies, List<MovieInfo> smallScreenMovies)
        {
            return new Billboard(startDate, bigScreenMovies, smallScreenMovies);
        }

    }
}
