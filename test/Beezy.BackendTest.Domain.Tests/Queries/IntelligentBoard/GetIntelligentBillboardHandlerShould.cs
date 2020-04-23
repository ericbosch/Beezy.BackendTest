using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Beezy.BackendTest.Domain.Proxies;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models;
using Beezy.BackendTest.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Optional;
using Xunit;

namespace Beezy.BackendTest.Domain.Tests.Queries.IntelligentBoard
{
    public class GetIntelligentBillboardHandlerShould
    {
        private readonly ITheMovieDbProxy _proxy;
        private readonly IBeezyCinemaRepository _repository;
        private readonly IDateService _dateService;

        public GetIntelligentBillboardHandlerShould()
        {
            _proxy = Substitute.For<ITheMovieDbProxy>();
            _repository = Substitute.For<IBeezyCinemaRepository>();
            _proxy.GetMovies().Returns(ProxyMoviesSeed().SomeNotNull());
            _repository.GetMovies().Returns(ProxyMoviesSeed().SomeNotNull());
            _dateService = Substitute.For<IDateService>();
            _dateService.Now().Returns(new DateTime(2020, 04, 22));
        }

        [Fact]
        public async Task BuildASingleBillboard()
        {
            //Arrange
            var handler = new GetIntelligentBillboardHandler(_proxy, _repository, _dateService);

            //Act
            var result = await handler.Handle(new GetIntelligentBillboardRequest(1, 2, 2, false), CancellationToken.None);

            var firstSmallScreen = result.Billboards.ValueOr(() => null).First().SmallScreenMovies.First();
            var secondSmallScreen = result.Billboards.ValueOr(() => null).First().SmallScreenMovies.Skip(1).First();
            var firstBigScreen = result.Billboards.ValueOr(() => null).First().BigScreenMovies.First();
            var secondBigScreen = result.Billboards.ValueOr(() => null).First().BigScreenMovies.Skip(1).First();

            //Assert
            result.Billboards.ValueOr(() => null).Should().HaveCount(1);
            result.Billboards.ValueOr(() => null).Should()
                .Match(b => b.First().StartDate == _dateService.Now())
                .And.Match(b => b.First().BigScreenMovies.Count == 2)
                .And.Match(b => b.First().SmallScreenMovies.Count == 2);
            firstSmallScreen.Title.Should().Be("Title1");
            secondSmallScreen.Title.Should().Be("Title2");
            firstBigScreen.Title.Should().Be("Title3");
            secondBigScreen.Title.Should().Be("Title4");
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 4)]
        [InlineData(4, 8)]
        [InlineData(8, 8)]
        [InlineData(16, 8)]
        public async Task BuildAsManyBillboardsAsManyWeeksAreRequested(int timePeriod, int moviesExpected)
        {
            //Arrange
            var handler = new GetIntelligentBillboardHandler(_proxy, _repository, _dateService);

            //Act
            var result = await handler.Handle(new GetIntelligentBillboardRequest(timePeriod, 1, 1, false), CancellationToken.None);

            //Assert
            result.Billboards.ValueOr(() => null).Should().HaveCount(timePeriod);
            result.Billboards.ValueOr(() => null).Should().Match(b => b.First().StartDate == _dateService.Now());
            result.Billboards.ValueOr(() => null).Should().Match(b => GetTotalMovies(b) == moviesExpected);
        }

        private int GetTotalMovies(IEnumerable<Billboard> billboards)
        {
            return billboards.Sum(b => b.BigScreenMovies.Count + b.SmallScreenMovies.Count);
        }

        private List<MovieInfo> ProxyMoviesSeed()
        {
            return new List<MovieInfo>
            {
                new MovieInfo(
                    "Title1",
                    "Overview 1",
                    new List<string>(){"Action", "Adventure"},
                    "es",
                    new DateTime(2020, 4, 20),
                    800,
                    MovieInfo.SmallScreen
                ),
                new MovieInfo(
                    "Title2",
                    "Overview 2",
                    new List<string>(){"Action", "Adventure"},
                    "es",
                    new DateTime(2020, 4, 20),
                    700,
                    MovieInfo.SmallScreen
                ),
                new MovieInfo(
                    "Title3",
                    "Overview 3",
                    new List<string>(){"Action", "Adventure"},
                    "es",
                    new DateTime(2020, 4, 20),
                    600,
                    MovieInfo.BigScreen
                ),
                new MovieInfo(
                    "Title4",
                    "Overview 4",
                    new List<string>(){"Action", "Adventure"},
                    "es",
                    new DateTime(2020, 4, 20),
                    500,
                    MovieInfo.BigScreen
                ),
                new MovieInfo(
                    "Title5",
                    "Overview 5",
                    new List<string>(){"Action", "Adventure"},
                    "es",
                    new DateTime(2020, 4, 27),
                    400,
                    MovieInfo.SmallScreen
                ),
                new MovieInfo(
                    "Title6",
                    "Overview 6",
                    new List<string>(){"Action", "Adventure"},
                    "es",
                    new DateTime(2020, 4, 27),
                    300,
                    MovieInfo.SmallScreen
                ),
                new MovieInfo(
                    "Title7",
                    "Overview 7",
                    new List<string>(){"Action", "Adventure"},
                    "es",
                    new DateTime(2020, 4, 27),
                    200,
                    MovieInfo.BigScreen
                ),
                new MovieInfo(
                    "Title8",
                    "Overview 8",
                    new List<string>(){"Action", "Adventure"},
                    "es",
                    new DateTime(2020, 4, 27),
                    100,
                    MovieInfo.BigScreen
                )
            };
        }
    }
}
