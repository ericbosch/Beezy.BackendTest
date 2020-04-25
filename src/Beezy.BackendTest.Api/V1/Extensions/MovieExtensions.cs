using System.Collections.Generic;
using System.Linq;
using Beezy.BackendTest.Api.V1.Models.Recommendations;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models;

#pragma warning disable 1591

namespace Beezy.BackendTest.Api.V1.Extensions
{
    public static class MovieExtensions
    {
        public static IEnumerable<OnScreenMovieRecommendation> ToDto(this List<MovieInfo> movies)
        {
            return movies.Select((movie, index) => new OnScreenMovieRecommendation
            (
                index+1,
                new MovieRecommendationResponse()
                {
                    Overview = movie.Overview,
                    ReleaseDate = movie.ReleaseDate,
                    Genres = movie.Genres,
                    Keywords = new List<string>(),
                    Language = movie.Language,
                    Title = movie.Title,
                    WebSite = string.Empty,
                }

            ));
        }
    }
}
