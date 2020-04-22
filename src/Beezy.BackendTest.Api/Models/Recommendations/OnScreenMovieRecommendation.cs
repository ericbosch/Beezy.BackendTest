namespace Beezy.BackendTest.Api.Models.Recommendations
{
    /// <summary>
    /// Container model for suggested movies for each theater
    /// </summary>
    public class OnScreenMovieRecommendation
    {
        /// <summary>
        /// Screen identifier
        /// </summary>
        public int Screen { get; set; }
        /// <summary>
        /// Movie recommended to show
        /// </summary>
        public MovieRecommendationResponse Movie { get; set; }
    }
}
