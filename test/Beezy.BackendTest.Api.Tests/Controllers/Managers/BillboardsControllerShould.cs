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
                    Billboard.Create(new DateTime(2020, 4, 20),
                        new List<MovieInfo>()
                        {
                            MovieInfo.Create("Title1",
                                "Overview 1",
                                new List<string>() {"Action", "Adventure"},
                                "es",
                                new DateTime(2020, 4, 20),
                                800,
                                MovieInfo.SmallScreen),
                            MovieInfo.Create("Title2",
                                "Overview 2",
                                new List<string>() {"Action", "Adventure"},
                                "es",
                                new DateTime(2020, 4, 20),
                                700,
                                MovieInfo.SmallScreen)
                        },
                        new List<MovieInfo>()
                        {
                            MovieInfo.Create("Title3",
                                "Overview 3",
                                new List<string>() {"Action", "Adventure"},
                                "es",
                                new DateTime(2020, 4, 20),
                                600,
                                MovieInfo.BigScreen),
                            MovieInfo.Create("Title4",
                                "Overview 4",
                                new List<string>() {"Action", "Adventure"},
                                "es",
                                new DateTime(2020, 4, 20),
                                500,
                                MovieInfo.BigScreen)
                        }),
                    Billboard.Create(new DateTime(2020, 4, 27),
                        new List<MovieInfo>()
                        {
                            MovieInfo.Create("Title5",
                                "Overview 5",
                                new List<string>() {"Action", "Adventure"},
                                "es",
                                new DateTime(2020, 4, 27),
                                400,
                                MovieInfo.SmallScreen),
                            MovieInfo.Create("Title6",
                                "Overview 6",
                                new List<string>() {"Action", "Adventure"},
                                "es",
                                new DateTime(2020, 4, 27),
                                300,
                                MovieInfo.SmallScreen)
                        },
                        new List<MovieInfo>()
                        {
                            MovieInfo.Create("Title7",
                                "Overview 7",
                                new List<string>() {"Action", "Adventure"},
                                "es",
                                new DateTime(2020, 4, 27),
                                200,
                                MovieInfo.BigScreen),
                            MovieInfo.Create("Title8",
                                "Overview 8",
                                new List<string>() {"Action", "Adventure"},
                                "es",
                                new DateTime(2020, 4, 27),
                                100,
                                MovieInfo.BigScreen)
                        })
                }.SomeNotNull());
        }
        #endregion
    }
}
