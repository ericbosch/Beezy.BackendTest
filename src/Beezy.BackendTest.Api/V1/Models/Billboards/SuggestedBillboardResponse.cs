using System.Collections.Generic;
using Beezy.BackendTest.Api.V1.Models.Billboards.Base;
using Beezy.BackendTest.Api.V1.Models.Recommendations;

namespace Beezy.BackendTest.Api.V1.Models.Billboards
{
    /// <summary>
    /// Container model for suggested billboard
    /// </summary>
    public class SuggestedBillboardResponse : BaseBillboardResponse
    {
        /// <summary>
        /// Directory of movies to screen on the theater
        /// </summary>
        public List<OnScreenMovieRecommendation> OnScreenMovies { get; }
    }
}
