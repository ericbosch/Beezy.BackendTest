namespace Beezy.BackendTest.Api.Models
{
    /// <inheritdoc />
    public class TvShowsRecommendationResponse : BaseRecommendationResponse
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
