using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beezy.BackendTest.Api.Models.Recommendations;

namespace Beezy.BackendTest.Api.Models.Billboards
{
    /// <summary>
    /// Container model for suggested billboard
    /// </summary>
    public class SuggestedBillboardResponse
    {
        /// <summary>
        /// Directory of movies to screen on the theater
        /// </summary>
        public List<OnScreenMoviesRecommendation> OnScreenMovies { get; set; }
    }
}
