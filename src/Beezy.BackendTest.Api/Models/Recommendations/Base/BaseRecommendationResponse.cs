using System;
using System.Collections.Generic;

namespace Beezy.BackendTest.Api.Models.Recommendations.Base
{
    /// <summary>
    /// Model Container for recommendations. It can either be movies, TV shows or documentaries.
    /// </summary>
    public abstract class BaseRecommendationResponse
    {
        /// <summary>
        /// Title of the movie, TV show or documentary
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Resume of the movie, TV show or documentary
        /// </summary>
        public string Overview { get; set; }
        /// <summary>
        /// Genres which belongs the movie, TV show or documentary
        /// </summary>
        public IReadOnlyList<string> Genres { get; set; }
        /// <summary>
        /// Language of the movie, TV show or documentary
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// Release date of the movie, TV show or documentary
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        /// <summary>
        /// Website of the movie, TV show or documentary
        /// </summary>
        public string WebSite { get; set; }
        /// <summary>
        /// Keywords used to identify the movie, TV show or documentary
        /// </summary>
        public IReadOnlyList<string> Keywords { get; set; }
    }
}