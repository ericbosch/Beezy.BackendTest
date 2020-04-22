using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Beezy.BackendTest.Domain.Proxies;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models;
using FluentAssertions;
using NSubstitute;
using Optional;
using Xunit;

namespace Beezy.BackendTest.Domain.Tests.Queries.IntelligentBoard
{
    public class GetIntelligentBillboardHandlerShould
    {
        private readonly ITheMovieDbProxy _proxy;
        private readonly IDateService _dateService;

        public GetIntelligentBillboardHandlerShould()
        {
            _proxy = Substitute.For<ITheMovieDbProxy>();
            _proxy.GetMovies().Returns(ProxyMoviesSeed().SomeNotNull());
            _dateService = Substitute.For<IDateService>();
            _dateService.Now().Returns(new DateTime(2020, 04, 22));
        }

        [Fact]
        public async Task BuildASingleBillboard()
        {
            //Arrange
            var handler = new GetIntelligentBillboardHandler(_proxy, _dateService);

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
            firstSmallScreen.Id.Should().Be(1);
            firstSmallScreen.Title.Should().Be("Title1");
            secondSmallScreen.Id.Should().Be(2);
            secondSmallScreen.Title.Should().Be("Title2");
            firstBigScreen.Id.Should().Be(3);
            firstBigScreen.Title.Should().Be("Title3");
            secondBigScreen.Id.Should().Be(4);
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
            var handler = new GetIntelligentBillboardHandler(_proxy, _dateService);

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

        private List<Movie> ProxyMoviesSeed()
        {
            return new List<Movie>
            {
                new Movie
                {
                    Adult = false, BackdropPath = "path1",
                    GenreIds = new List<int> {1},
                    Id = 1,
                    OriginalLanguage = "es",
                    OriginalTitle = "Original Title 1",
                    Overview = "Overview 1",
                    Genres = {"1"}, Title = "Title1", ReleaseDate = new DateTime(2020, 4, 20),
                    VoteCount = 1, Video = false, PosterPath = "PosterPath1", VoteAverage = 1,
                    Popularity = 1,
                    ScreenSize = ScreenSize.Small
                },
                new Movie
                {
                    Adult = false, BackdropPath = "path2",
                    GenreIds = new List<int> {2},
                    Id = 2,
                    OriginalLanguage = "es",
                    OriginalTitle = "Original Title 2",
                    Overview = "Overview 2",
                    Genres = {"2"}, Title = "Title2", ReleaseDate = new DateTime(2020, 4, 20),
                    VoteCount = 2, Video = false, PosterPath = "PosterPath2", VoteAverage = 1,
                    Popularity = 2,
                    ScreenSize = ScreenSize.Small
                },
                new Movie
                {
                    Adult = false, BackdropPath = "path3",
                    GenreIds = new List<int> {3},
                    Id = 3,
                    OriginalLanguage = "es",
                    OriginalTitle = "Original Title 3",
                    Overview = "Overview 3",
                    Genres = {"3"}, Title = "Title3", ReleaseDate = new DateTime(2020, 4, 20),
                    VoteCount = 300, Video = false, PosterPath = "PosterPath1", VoteAverage = 300,
                    Popularity = 300,
                    ScreenSize = ScreenSize.Big
                },
                new Movie
                {
                    Adult = false, BackdropPath = "path4",
                    GenreIds = new List<int> {4},
                    Id = 4,
                    OriginalLanguage = "es",
                    OriginalTitle = "Original Title 4",
                    Overview = "Overview 4",
                    Genres = {"4"}, Title = "Title4", ReleaseDate = new DateTime(2020, 4, 20),
                    VoteCount = 400, Video = false, PosterPath = "PosterPath4", VoteAverage = 400,
                    Popularity = 400,
                    ScreenSize = ScreenSize.Big
                },
                new Movie
                {
                    Adult = false, BackdropPath = "path5",
                    GenreIds = new List<int> {5},
                    Id = 5,
                    OriginalLanguage = "es",
                    OriginalTitle = "Original Title 5",
                    Overview = "Overview 5",
                    Genres = {"5"}, Title = "Title5", ReleaseDate = new DateTime(2020, 4, 27),
                    VoteCount = 5, Video = false, PosterPath = "PosterPath5", VoteAverage = 5,
                    Popularity = 5,
                    ScreenSize = ScreenSize.Small
                },
                new Movie
                {
                    Adult = false, BackdropPath = "path6",
                    GenreIds = new List<int> {6},
                    Id = 6,
                    OriginalLanguage = "es",
                    OriginalTitle = "Original Title 6",
                    Overview = "Overview 6",
                    Genres = {"6"}, Title = "Title6", ReleaseDate = new DateTime(2020, 4, 27),
                    VoteCount = 6, Video = false, PosterPath = "PosterPath6", VoteAverage = 6,
                    Popularity = 6,
                    ScreenSize = ScreenSize.Small
                },
                new Movie
                {
                    Adult = false, BackdropPath = "path7",
                    GenreIds = new List<int> {7},
                    Id = 7,
                    OriginalLanguage = "es",
                    OriginalTitle = "Original Title 7",
                    Overview = "Overview 7",
                    Genres = {"7"}, Title = "Title7", ReleaseDate = new DateTime(2020, 4, 27),
                    VoteCount = 700, Video = false, PosterPath = "PosterPath7", VoteAverage = 700,
                    Popularity = 700,
                    ScreenSize = ScreenSize.Big
                },
                new Movie
                {
                    Adult = false, BackdropPath = "path8",
                    GenreIds = new List<int> {8},
                    Id = 8,
                    OriginalLanguage = "es",
                    OriginalTitle = "Original Title 8",
                    Overview = "Overview 8",
                    Genres = {"8"}, Title = "Title8", ReleaseDate = new DateTime(2020, 4, 27),
                    VoteCount = 800, Video = false, PosterPath = "PosterPath8", VoteAverage = 800,
                    Popularity = 800,
                    ScreenSize = ScreenSize.Big
                }
            };
        }
    }
}
