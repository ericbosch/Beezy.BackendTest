using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beezy.BackendTest.Api.Models.Recommendations;

namespace Beezy.BackendTest.Api.Models.Billboards
{
    /// <summary>
    /// Container model for suggested intelligent billboard
    /// </summary>
    public class IntelligentBillboardResponse
    {
        /// <summary>
        /// Directory of movies to screen on big rooms on the theater
        /// </summary>
        public List<OnScreenMoviesRecommendation> BigRoomMovies { get; set; }

        /// <summary>
        /// Directory of movies to screen on small rooms on the theater
        /// </summary>
        public List<OnScreenMoviesRecommendation> SmallRoomMovies { get; set; }
    }
}
