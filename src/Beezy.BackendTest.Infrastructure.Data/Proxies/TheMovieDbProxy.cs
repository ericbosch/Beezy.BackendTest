using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beezy.BackendTest.Domain.Proxies;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models;
using Beezy.BackendTest.Infrastructure.CrossCutting.Settings;
using Beezy.BackendTest.Infrastructure.Data.Models;
using Optional;
using ServiceStack;

namespace Beezy.BackendTest.Infrastructure.Data.Proxies
{
    public class TheMovieDbProxy : ITheMovieDbProxy
    {
        private readonly TheMovieDbApiSettings _settings;

        public TheMovieDbProxy(TheMovieDbApiSettings settings)
        {
            _settings = settings;
        }

        public async Task<Option<List<Movie>>> GetMovies()
        {
            string movieDiscoverRequest = $"{_settings.Url}/discover/movie";

            var request = movieDiscoverRequest
                .AddQueryParam("api_key", _settings.ApiKey);

            var result = await GetMoviesForScreenSize(request, page: 1, ScreenSize.Big);
            result.AddRange(await GetMoviesForScreenSize(request, page: 2, ScreenSize.Small));

            return result.SomeNotNull();
        }

        private async Task<List<Movie>> GetMoviesForScreenSize(string request, int page, ScreenSize screenSize)
        {
            var apiResult = await request.AddQueryParam("page", page).GetJsonFromUrlAsync();
            var tmdbResponse = apiResult.FromJson<TheMovieDbResponse>();

            return tmdbResponse?.Results?.Select(movie => new Movie
            {
                Adult = movie.Adult,
                BackdropPath = movie.BackdropPath,
                GenreIds = movie.GenreIds,
                Id = movie.Id,
                OriginalLanguage = movie.OriginalLanguage,
                OriginalTitle = movie.OriginalTitle,
                Overview = movie.Overview,
                Popularity = movie.Popularity,
                PosterPath = movie.PosterPath,
                ReleaseDate = movie.ReleaseDate,
                Title = movie.Title,
                Video = movie.Video,
                VoteAverage = movie.VoteAverage,
                VoteCount = movie.VoteCount,
                ScreenSize = screenSize
            }).ToList();
        }
    }
}
