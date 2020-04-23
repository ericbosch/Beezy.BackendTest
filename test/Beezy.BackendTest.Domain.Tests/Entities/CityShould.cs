using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture.Xunit2;
using Beezy.BackendTest.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Beezy.BackendTest.Domain.Tests.Entities
{
    public class CityShould
    {
        [Theory, AutoData]
        public void GetACityWithExpectedContent(int id, string name, int population)
        {
            //Arrange + Act
            var city = new City(id, name, population);

            //Assert
            city.Id.Should().Be(id);
            city.Name.Should().Be(name);
            city.Population.Should().Be(population);
            city.Cinema.Should().NotBeNull();
        }
    }
}
