using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture.Xunit2;
using Beezy.BackendTest.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Beezy.BackendTest.Domain.Tests.Entities
{
    public class RoomShould
    {
        [Theory, AutoData]
        public void GetARoomWithExpectedContent(int id, string name, string size, int seats, int cinemaId, string cinemaName, DateTime cinemaOpenSince, int cityId)
        {
            //Arrange + Act
            var room = new Room(id, name, size, seats, cinemaId,
                new Cinema(cinemaId, cinemaName, cinemaOpenSince, null, cityId));

            //Assert
            room.Id.Should().Be(id);
            room.Name.Should().Be(name);
            room.Size.Should().Be(size);
            room.Seats.Should().Be(seats);
            room.CinemaId.Should().Be(cinemaId);
            room.Cinema.Name.Should().Be(cinemaName);
            room.Cinema.OpenSince.Should().Be(cinemaOpenSince);
            room.Cinema.CityId.Should().Be(cityId);
            room.Session.Should().NotBeNull();
        }
    }
}
