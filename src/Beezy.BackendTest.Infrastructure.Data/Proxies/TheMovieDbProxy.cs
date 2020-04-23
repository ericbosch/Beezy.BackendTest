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

        public async Task<Option<List<MovieInfo>>> GetMovies()
        {
            string movieDiscoverRequest = $"{_settings.Url}/discover/movie";

            var request = movieDiscoverRequest
                .AddQueryParam("api_key", _settings.ApiKey);

            var result = await GetMoviesForScreenSize(request, page: 1, MovieInfo.BigScreen);
            result.AddRange(await GetMoviesForScreenSize(request, page: 2, MovieInfo.SmallScreen));

            return result.SomeNotNull();
        }

        private async Task<List<MovieInfo>> GetMoviesForScreenSize(string request, int page, ScreenSize screenSize)
        {
            var apiResult = await request.AddQueryParam("page", page).GetJsonFromUrlAsync();
            var tmdbResponse = apiResult.FromJson<TheMovieDbResponse>();

            return tmdbResponse?.Results?.Select(movie => new MovieInfo
            (
                movie.Title,
                movie.Overview,
                GetMovieGenres(movie.GenreIds),
                movie.OriginalLanguage,
                movie.ReleaseDate,
                movie.VoteCount,
                screenSize
            )).ToList();
        }

        private IReadOnlyList<string> GetMovieGenres(List<int> movieGenreIds)
        {
            return movieGenreIds.Select(g => g.ToString()).ToList();
        }
    }
}
