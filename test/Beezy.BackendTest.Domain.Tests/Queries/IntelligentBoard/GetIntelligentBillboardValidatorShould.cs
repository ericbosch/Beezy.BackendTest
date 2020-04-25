using Beezy.BackendTest.Domain.Queries.IntelligentBillboard;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Beezy.BackendTest.Domain.Tests.Queries.IntelligentBoard
{
    public class GetIntelligentBillboardValidatorShould
    {
        [Theory]
        [InlineData(0, 0, 0, false)]
        [InlineData(1, 0, 0, true)]
        [InlineData(0, 1, 0, false)]
        [InlineData(0, 0, 1, false)]
        [InlineData(-1, 1, 1, false)]
        [InlineData(1, -1, 1, false)]
        [InlineData(1, 1, -1, false)]
        [InlineData(1, 1, 1, true)]
        public void Validate_The_Request(int timePeriod, int bigRooms, int smallRooms, bool valid)
        {
            var validator = new GetIntelligentBillboardValidator();
            var request = new GetIntelligentBillboardRequest(timePeriod, bigRooms, smallRooms, "anyCity");

            var result = validator.Validate(request);

            result.IsValid.Should().Be(valid);
        }
    }
}
