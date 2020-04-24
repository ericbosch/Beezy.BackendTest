using AutoFixture.Xunit2;
using Beezy.BackendTest.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Beezy.BackendTest.Domain.Tests.Entities
{
    public class GenreShould
    {
        [Theory, AutoData]
        public void GetAGenreWithExpectedContent(int id, string name)
        {
            //Arrange + Act
            var genre = new Genre(id, name);

            //Assert
            genre.Id.Should().Be(id);
            genre.Name.Should().Be(name);
        }
    }
}
