using System;
using System.Collections.Generic;
using Beezy.BackendTest.Api.Controllers.Managers;
using Beezy.BackendTest.Api.Models.Billboards;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Optional;
using Xunit;

namespace Beezy.BackendTest.Api.Tests.Controllers.Managers
{
    public class BillboardsControllerShould
    {
        private readonly IMediator _mediator;

        public BillboardsControllerShould()
        {
            _mediator = Substitute.For<IMediator>();
            _mediator.Send(Arg.Any<GetIntelligentBillboardRequest>()).Returns(IntelligentBillboardResultSeed());
        }

        [Fact]
        public async void AskForAnIntelligentBillboard()
        {
            //Arrange
            var controller = new BillboardsController(_mediator);

            //Act
            var result = await controller.GetIntelligentBillboard(2, 2, 2, true);

            //Assert
            var expectedType = (List<IntelligentBillboardResponse>)Assert.IsType<OkObjectResult>(result).Value;
            expectedType.Count.Should().Be(2);
        }

        #region SeedData
        private GetIntelligentBillboardResponse IntelligentBillboardResultSeed()
        {
            return new GetIntelligentBillboardResponse(
                new List<Billboard>
                {
                    new Billboard
                    {
                        StartDate = new DateTime(2020, 4, 20),
                        SmallScreenMovies = new List<Movie>()
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
                                Popularity = 1
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
                                Popularity = 2
                            }
                        },
                        BigScreenMovies = new List<Movie>()
                        {
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
                                Popularity = 300
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
                                Popularity = 400
                            }
                        }
                    },
                    new Billboard
                    {
                        StartDate = new DateTime(2020, 4, 27),
                        SmallScreenMovies = new List<Movie>()
                        {
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
                                Popularity = 5
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
                                Popularity = 6
                            }
                        },
                        BigScreenMovies = new List<Movie>()
                        {
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
                                Popularity = 700
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
                                Popularity = 800
                            }
                        }
                    }
                }.SomeNotNull());
        }
        #endregion
    }
}
