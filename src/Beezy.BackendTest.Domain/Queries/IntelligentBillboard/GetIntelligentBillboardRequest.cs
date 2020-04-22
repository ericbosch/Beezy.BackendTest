using MediatR;

namespace Beezy.BackendTest.Domain.Queries.IntelligentBillboard
{
    public class GetIntelligentBillboardRequest : IRequest<GetIntelligentBillboardResponse>
    {
        public int TimePeriod { get; }
        public int BigRooms { get; }
        public int SmallRooms { get; }
        public bool BasedOnCity { get; }

        public GetIntelligentBillboardRequest(int timePeriod, int bigRooms, int smallRooms, bool basedOnCity)
        {
            TimePeriod = timePeriod;
            BigRooms = bigRooms;
            SmallRooms = smallRooms;
            BasedOnCity = basedOnCity;
        }
    }
}
