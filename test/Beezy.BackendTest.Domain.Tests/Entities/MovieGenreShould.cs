using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture.Xunit2;
using Beezy.BackendTest.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Beezy.BackendTest.Domain.Tests.Entities
{
    public class MovieGenreShould
    {
        [Theory, AutoData]
        public void GetAMovieGenreWithExpectedContent(int movieId, int genreId)
        {
            //Arrange + Act
            var movieGenre = new MovieGenre(movieId, genreId);

            //Assert
            movieGenre.MovieId.Should().Be(movieId);
            movieGenre.GenreId.Should().Be(genreId);
        }
    }
}
