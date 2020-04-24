using System;
using AutoFixture.Xunit2;
using Beezy.BackendTest.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Beezy.BackendTest.Domain.Tests.Entities
{
    public class MovieShould
    {
        [Theory, AutoData]
        public void GetAMovieWithExpectedContent(int id, string originalTitle, DateTime releaseDate, string originalLanguage, bool adult)
        {
            //Arrange + Act
            var movie = new Movie(id, originalTitle, releaseDate, originalLanguage, adult);

            //Assert
            movie.Id.Should().Be(id);
            movie.OriginalTitle.Should().Be(originalTitle);
            movie.ReleaseDate.Should().Be(releaseDate);
            movie.OriginalLanguage.Should().Be(originalLanguage);
            movie.Adult.Should().Be(adult);
            movie.Session.Should().NotBeNull();
        }
    }
}
