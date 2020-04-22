using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Beezy.BackendTest.Domain.Extensions;
using Beezy.BackendTest.Domain.Proxies;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models;
using Optional;

namespace Beezy.BackendTest.Domain.Queries.IntelligentBillboard
{
    public class GetIntelligentBillboardHandler : IRequestHandler<GetIntelligentBillboardRequest, GetIntelligentBillboardResponse>
    {
        private readonly ITheMovieDbProxy _proxy;
        private readonly IDateService _dateService;

        public GetIntelligentBillboardHandler(ITheMovieDbProxy proxy, IDateService dateService)
        {
            _proxy = proxy;
            _dateService = dateService;
        }

        public async Task<GetIntelligentBillboardResponse> Handle(GetIntelligentBillboardRequest request,
            CancellationToken cancellationToken)
        {
            var movies = await _proxy.GetMovies();

            return movies.Match(
                response => new GetIntelligentBillboardResponse(billboards: BuildBillboards(request, response).ToList()
                    .SomeNotNull()),
                () => new GetIntelligentBillboardResponse(billboards: new Option<List<Billboard>>()));
        }

        private IEnumerable<Billboard> BuildBillboards(GetIntelligentBillboardRequest request, List<Movie> movies)
        {
            var startDate = _dateService.Now();
            var weekLength = 7;
            var totalWeeks = request.TimePeriod;

            for (int week = 0; week < totalWeeks; week++)
            {
                var billboard = new Billboard
                {
                    StartDate = startDate,
                    BigScreenMovies = GetMoviesWithValidReleaseDateAndSize(movies, request.BigRooms, startDate, ScreenSize.Big).ValueOr(() => new List<Movie>()),
                    SmallScreenMovies = GetMoviesWithValidReleaseDateAndSize(movies, request.SmallRooms, startDate, ScreenSize.Small).ValueOr(() => new List<Movie>()),
                };
                movies = movies.Where(m =>
                    !billboard.BigScreenMovies.Contains(m) && !billboard.SmallScreenMovies.Contains(m)).ToList();
                startDate = startDate.AddDays(weekLength);
                yield return billboard;
            }
        }

        private static Option<List<Movie>> GetMoviesWithValidReleaseDateAndSize(IReadOnlyCollection<Movie> movies, int roomNumber, DateTime startDate, ScreenSize screenSize)
        {
            if (movies?.Count == 0) return new Option<List<Movie>>();
            return movies
                .WithScreenSize(screenSize)
                .ThatCanBeReleasedAtDate(startDate)
                .Take(roomNumber)
                .ToList().SomeNotNull();
        }
    }
}
