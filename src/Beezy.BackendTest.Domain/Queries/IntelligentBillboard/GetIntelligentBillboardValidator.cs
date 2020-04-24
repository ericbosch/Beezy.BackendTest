using FluentValidation;

namespace Beezy.BackendTest.Domain.Queries.IntelligentBillboard
{
    public class GetIntelligentBillboardValidator : AbstractValidator<GetIntelligentBillboardRequest>
    {
        public GetIntelligentBillboardValidator()
        {
            RuleFor(r => r.BigRooms).NotNull().NotEmpty().GreaterThan(0)
                .WithMessage("Number of big rooms should be greater than 0");
            RuleFor(r => r.SmallRooms).NotNull().NotEmpty().GreaterThan(0)
                .WithMessage("Number of small rooms should be greater than 0");
            RuleFor(r => r.TimePeriod).NotNull().NotEmpty().GreaterThan(0)
                .WithMessage("Number of weeks should be greater than 0");
        }
    }
}
