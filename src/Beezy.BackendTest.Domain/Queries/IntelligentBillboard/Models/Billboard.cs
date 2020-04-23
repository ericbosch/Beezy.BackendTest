using System;
using System.Collections.Generic;

namespace Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models
{
    public class Billboard
    {
        public DateTime StartDate { get; set; }
        public List<MovieInfo> BigScreenMovies { get; set; }
        public List<MovieInfo> SmallScreenMovies { get; set; }
    }
}
