using System;
using AutoFixture.Xunit2;
using Beezy.BackendTest.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Beezy.BackendTest.Domain.Tests.Entities
{
    public class CinemaShould
    {
        [Theory, AutoData]
        public void GetACinemaWithExpectedContent(int id, string name, DateTime openSince, int cityId, string cityName, int cityPopulation)
        {
            //Arrange + Act
            var cinema = new Cinema(id, name, openSince, new City(cityId, cityName, cityPopulation), cityId);

            //Assert
            cinema.Id.Should().Be(id);
            cinema.Name.Should().Be(name);
            cinema.OpenSince.Should().Be(openSince);
            cinema.CityId.Should().Be(cityId);
            cinema.City.Id.Should().Be(cityId);
            cinema.City.Name.Should().Be(cityName);
            cinema.City.Population.Should().Be(cityPopulation);
            cinema.Room.Should().NotBeNull();
        }
    }
}
