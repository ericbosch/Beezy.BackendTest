using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Beezy.BackendTest.Api.Models.Billboards;
using Beezy.BackendTest.Api.Tests.IntegrationTests.BogusData.Entities;
using Beezy.BackendTest.Api.Tests.IntegrationTests.BogusData.Models;
using Beezy.BackendTest.Domain;
using Beezy.BackendTest.Domain.Entities;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NSubstitute;
using ServiceStack;
using Xunit;

namespace Beezy.BackendTest.Api.Tests.IntegrationTests.Controllers.Managers
{
    public class BillboardsControllerShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly IDateService _dateService;

        public BillboardsControllerShould(CustomWebApplicationFactory<Startup> factory)
        {
            _dateService = Substitute.For<IDateService>();
            _dateService.Now().Returns(new DateTime(2020, 04, 22));
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task ReturnMoviesFromDatabase()
        {
            var billboardRequest = BogusIntelligentBillboardData.GetIntelligentBillboardRequestsList(10);


            var firstRequestExpected = billboardRequest.First();
            var city = BogusCityData.GetCity(firstRequestExpected.City);
            var cinemas = BogusCinemaData.GetCinemaList(100);
            var movies = BogusMovieData.GetMovieList(100);
            var sessions = BogusSessionData.GetSessionList(100, _dateService.Now());
            var rooms = BogusRoomData.GetRoomList(100);
            var genres = BogusGenreData.GetGenreList(100);
            var movieGenres = BogusMovieGenreData.GetMovieGenreList(100);

            await SeedData(city, cinemas, movies, sessions, rooms, genres, movieGenres);

            var request = new GetIntelligentBillboardRequest(
                firstRequestExpected.TimePeriod,
                firstRequestExpected.BigRooms,
                firstRequestExpected.SmallRooms,
                firstRequestExpected.City);

            var result = await CallApi(request);

            result.Should().HaveCount(request.TimePeriod);
            result.Should().OnlyHaveUniqueItems();
            result.Should().Contain(b => b.SmallScreenMovies.Count == request.SmallRooms);
            result.Should().Contain(b => b.BigScreenMovies.Count == request.BigRooms);
            result.Select(r => r.StartDate).Should().BeInAscendingOrder();
        }

        [Fact]
        public async Task ReturnMoviesFromProxy()
        {
            var billboardRequest = BogusIntelligentBillboardData.GetIntelligentBillboardRequestsList(5);
            var firstRequestExpected = billboardRequest.First();

            var request = new GetIntelligentBillboardRequest(
                firstRequestExpected.TimePeriod, 
                firstRequestExpected.BigRooms, 
                firstRequestExpected.SmallRooms, 
                null);

            var result = await CallApi(request);

            result.Should().HaveCount(request.TimePeriod);
            result.Should().OnlyHaveUniqueItems();
            result.Should().Contain(b => b.SmallScreenMovies.Count == request.SmallRooms);
            result.Should().Contain(b => b.BigScreenMovies.Count == request.BigRooms);
            result.Select(r => r.StartDate).Should().BeInAscendingOrder();
        }

        private async Task<List<IntelligentBillboardResponse>> CallApi(GetIntelligentBillboardRequest request)
        {
            var response = await _client.GetAsync($"/api/managers/billboards/intelligent{BuildQueryParams(request)}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var result =
                JsonConvert.DeserializeObject<List<IntelligentBillboardResponse>>(jsonResponse);
            return result;
        }

        private async Task SeedData(City city, IList<Cinema> cinemas, IList<Movie> movies,
            IList<Session> sessions, IList<Room> rooms, IList<Genre> genres, IList<MovieGenre> movieGenres)
        {
            PickCityIdForCinemas(cinemas, city.Id);
            PickRandomCinemaIdForRooms(rooms, cinemas.Min(c => c.Id), cinemas.Max(c => c.Id));
            PickRandomMovieIdForSessions(sessions, movies.Min(m => m.Id), movies.Max(m => m.Id));
            PickRandomRoomIdForSessions(sessions, rooms.Min(r => r.Id), rooms.Max(r => r.Id));
            PickRandomMovieAndGenreIdForMovieGenres(movieGenres, movies.Min(m => m.Id), movies.Max(m => m.Id),
                genres.Min(g => g.Id), genres.Max(g => g.Id));

            await _factory.ExecuteDbContextAsync(async context =>
            {
                await context.MovieGenre.AddRangeAsync(movieGenres);
                await context.Genre.AddRangeAsync(genres);
                await context.Movie.AddRangeAsync(movies);
                await context.Room.AddRangeAsync(rooms);
                await context.Session.AddRangeAsync(sessions);
                await context.City.AddAsync(city);
                await context.Cinema.AddRangeAsync(cinemas);
                await context.SaveChangesAsync();
            });
        }

        private void PickRandomMovieIdForSessions(IList<Session> sessions, int minMovieIndex, int maxMovieIndex)
        {
            foreach (var session in sessions)
            {
                session.MovieId = new Faker().Random.Int(minMovieIndex, maxMovieIndex);
            }
        }

        private void PickRandomRoomIdForSessions(IList<Session> sessions, int minRoomIndex, int maxRoomIndex)
        {
            foreach (var session in sessions)
            {
                session.RoomId = new Faker().Random.Int(minRoomIndex, maxRoomIndex);
            }
        }

        private void PickRandomMovieAndGenreIdForMovieGenres(IList<MovieGenre> movieGenres, int minMovieIndex, int maxMovieIndex, int minGenreIndex, int maxGenreIndex)
        {
            int movieGenreIndex = 0;
            do
            {
                int movieId = new Faker().Random.Int(minMovieIndex, maxMovieIndex);
                int genreId = new Faker().Random.Int(minGenreIndex, maxGenreIndex);

                if (!movieGenres.Any(mg => mg.MovieId == movieId && mg.GenreId == genreId))
                {
                    movieGenres[movieGenreIndex].MovieId = movieId;
                    movieGenres[movieGenreIndex].GenreId = genreId;
                    movieGenreIndex++;
                }
            } while (movieGenreIndex < movieGenres.Count);
        }

        private void PickCityIdForCinemas(IList<Cinema> cinemas, int cityId)
        {
            foreach (var cinema in cinemas)
            {
                cinema.CityId = cityId;
            }
        }

        private void PickRandomCinemaIdForRooms(IList<Room> rooms, int minCinemaIndex, int maxCinemaIndex)
        {
            foreach (var room in rooms)
            {
                room.CinemaId = new Faker().Random.Int(minCinemaIndex, maxCinemaIndex);
            }
        }

        private string BuildQueryParams(GetIntelligentBillboardRequest request)
        {
            return string.Empty
                .AddQueryParam("timePeriod", request.TimePeriod)
                .AddQueryParam("bigRooms", request.BigRooms)
                .AddQueryParam("smallRooms", request.SmallRooms)
                .AddQueryParam("city", request.City)
                .AddQueryParam("api-version", "1.0")
                ;
        }
    }
}
