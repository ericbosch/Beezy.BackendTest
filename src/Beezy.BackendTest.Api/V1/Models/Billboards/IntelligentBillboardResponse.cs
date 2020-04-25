using System;
using System.Collections.Generic;
using Beezy.BackendTest.Api.V1.Models.Billboards.Base;
using Beezy.BackendTest.Api.V1.Models.Recommendations;

namespace Beezy.BackendTest.Api.V1.Models.Billboards
{
    /// <summary>
    /// Container model for suggested intelligent billboard
    /// </summary>
    public class IntelligentBillboardResponse : BaseBillboardResponse
    {

        /// <summary>
        /// Directory of movies to screen on big rooms on the theater
        /// </summary>
        public List<OnScreenMovieRecommendation> BigScreenMovies { get; set; }

        /// <summary>
        /// Directory of movies to screen on small rooms on the theater
        /// </summary>
        public List<OnScreenMovieRecommendation> SmallScreenMovies { get; set; }

        /// <inheritdoc />
        public IntelligentBillboardResponse()
        {
        }

        /// <inheritdoc />
        private IntelligentBillboardResponse(DateTime startDate, List<OnScreenMovieRecommendation> bigScreenMovies, List<OnScreenMovieRecommendation> smallScreenMovies)
        {
            StartDate = startDate;
            BigScreenMovies = bigScreenMovies;
            SmallScreenMovies = smallScreenMovies;
        }

        /// <summary>
        /// Factory for IntelligentBillboardResponse
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="bigScreenMovies"></param>
        /// <param name="smallScreenMovies"></param>
        /// <returns></returns>
        public static IntelligentBillboardResponse Create(DateTime startDate, List<OnScreenMovieRecommendation> bigScreenMovies, List<OnScreenMovieRecommendation> smallScreenMovies)
        {
            return new IntelligentBillboardResponse(startDate, bigScreenMovies, smallScreenMovies);
        }
    }
}
