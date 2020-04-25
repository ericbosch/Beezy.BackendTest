#pragma warning disable 1591
namespace Beezy.BackendTest.Api.V1.Models.Recommendations
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

        public OnScreenMovieRecommendation()
        {

        }

        public OnScreenMovieRecommendation(int screen, MovieRecommendationResponse movie)
        {
            Screen = screen;
            Movie = movie;
        }
    }
}
