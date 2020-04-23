using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture.Xunit2;
using Beezy.BackendTest.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Beezy.BackendTest.Domain.Tests.Entities
{
    public class SessionShould
    {
        [Theory, AutoData]
        public void GetASessionWithExpectedContent(int id, int roomId, int movieId, DateTime startTime, DateTime endTime, int? seatsSold,
            string movieOriginalTitle, DateTime movieReleaseDate, string movieOriginalLanguage, bool movieAdult,
            string roomName, string roomSize, int roomSeats, int roomCinemaId)
        {
            //Arrange + Act
            var session = new Session(id, roomId, movieId, startTime, endTime, seatsSold,
                new Movie(movieId, movieOriginalTitle, movieReleaseDate, movieOriginalLanguage, movieAdult),
                new Room(roomId, roomName, roomSize, roomSeats, roomCinemaId, null));

            //Assert
            session.Id.Should().Be(id);
            session.RoomId.Should().Be(roomId);
            session.MovieId.Should().Be(movieId);
            session.StartTime.Should().Be(startTime);
            session.EndTime.Should().Be(endTime);
            session.SeatsSold.Should().Be(seatsSold);
            session.Movie.Id.Should().Be(movieId);
            session.Room.Id.Should().Be(roomId);
        }
    }
}
