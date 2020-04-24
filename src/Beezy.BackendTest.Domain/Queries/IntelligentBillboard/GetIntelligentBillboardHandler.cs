using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Beezy.BackendTest.Domain.Extensions;
using Beezy.BackendTest.Domain.Proxies;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models;
using Beezy.BackendTest.Domain.Repositories;
using Optional;

namespace Beezy.BackendTest.Domain.Queries.IntelligentBillboard
{
    public class GetIntelligentBillboardHandler : IRequestHandler<GetIntelligentBillboardRequest, GetIntelligentBillboardResponse>
    {
        private readonly ITheMovieDbProxy _proxy;
        private readonly IBeezyCinemaRepository _repository;
        private readonly IDateService _dateService;

        public GetIntelligentBillboardHandler(ITheMovieDbProxy proxy, IBeezyCinemaRepository repository, IDateService dateService)
        {
            _proxy = proxy;
            _repository = repository;
            _dateService = dateService;
        }

        public async Task<GetIntelligentBillboardResponse> Handle(GetIntelligentBillboardRequest request,
            CancellationToken cancellationToken)
        {
            var movies = await GetMovies(request.City);

            return movies.Match(
                response => new GetIntelligentBillboardResponse(billboards: BuildBillboards(request, response).ToList()
                    .SomeNotNull()),
                () => new GetIntelligentBillboardResponse(billboards: new Option<List<Billboard>>()));
        }

        private async Task<Option<List<MovieInfo>>> GetMovies(string city)
        {
            if(string.IsNullOrWhiteSpace(city)) return await _proxy.GetMovies(); 
            return await _repository.GetMovies(city);
            
        }


        private IEnumerable<Billboard> BuildBillboards(GetIntelligentBillboardRequest request, List<MovieInfo> movies)
        {
            var startDate = _dateService.Now();
            var weekLength = 7;
            var totalWeeks = request.TimePeriod;

            for (int week = 0; week < totalWeeks; week++)
            {
                var billboard = Billboard.Create(startDate,
                    GetMoviesWithValidReleaseDateAndSize(movies, request.BigRooms, startDate, MovieInfo.BigScreen)
                        .ValueOr(() => new List<MovieInfo>()),
                    GetMoviesWithValidReleaseDateAndSize(movies, request.SmallRooms, startDate, MovieInfo.SmallScreen)
                        .ValueOr(() => new List<MovieInfo>()));
                movies = movies.Where(m =>
                    !billboard.BigScreenMovies.Contains(m) && !billboard.SmallScreenMovies.Contains(m)).ToList();
                startDate = startDate.AddDays(weekLength);
                yield return billboard;
            }
        }

        private static Option<List<MovieInfo>> GetMoviesWithValidReleaseDateAndSize(IReadOnlyCollection<MovieInfo> movies, int roomNumber, DateTime startDate, ScreenSize screenSize)
        {
            if (movies?.Count == 0) return new Option<List<MovieInfo>>();
            return movies
                .WithScreenSize(screenSize)
                .ThatCanBeReleasedAtDate(startDate)
                .Take(roomNumber)
                .ToList().SomeNotNull();
        }
    }
}
