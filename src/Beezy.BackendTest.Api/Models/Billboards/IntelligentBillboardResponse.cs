using System.Collections.Generic;
using Beezy.BackendTest.Api.Models.Billboards.Base;
using Beezy.BackendTest.Api.Models.Recommendations;

namespace Beezy.BackendTest.Api.Models.Billboards
{
    /// <summary>
    /// Container model for suggested intelligent billboard
    /// </summary>
    public class IntelligentBillboardResponse :BaseBillboardResponse
    {
        /// <summary>
        /// Directory of movies to screen on big rooms on the theater
        /// </summary>
        public List<OnScreenMovieRecommendation> BigScreenMovies { get; set; }

        /// <summary>
        /// Directory of movies to screen on small rooms on the theater
        /// </summary>
        public List<OnScreenMovieRecommendation> SmallScreenMovies { get; set; }
    }
}
