namespace Beezy.BackendTest.Api.Models
{
    public class TvShowsRecommendationResponse : BaseRecommendationResponse
    {
        public int Seasons { get; set; }
        public int Episodes { get; set; }
        public bool Concluded { get; set; }
    }
}
