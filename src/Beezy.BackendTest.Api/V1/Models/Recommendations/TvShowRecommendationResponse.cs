using Beezy.BackendTest.Api.V1.Models.Recommendations.Base;

namespace Beezy.BackendTest.Api.V1.Models.Recommendations
{
    /// <inheritdoc />
    public class TvShowRecommendationResponse : BaseRecommendationResponse
    {
        /// <summary>
        /// Number of seasons of the TV show.
        /// </summary>
        public int Seasons { get; set; }
        /// <summary>
        /// Number of episodes of each season.
        /// </summary>
        public int Episodes { get; set; }
        /// <summary>
        /// Indicates if the TV show are concluded or not.
        /// </summary>
        public bool Concluded { get; set; }
    }
}
