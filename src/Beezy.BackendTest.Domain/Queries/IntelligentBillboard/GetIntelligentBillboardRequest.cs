using MediatR;

namespace Beezy.BackendTest.Domain.Queries.IntelligentBillboard
{
    public class GetIntelligentBillboardRequest : IRequest<GetIntelligentBillboardResponse>
    {
        public int TimePeriod { get; protected set; }
        public int BigRooms { get; protected set; }
        public int SmallRooms { get; protected set; }
        public bool BasedOnCity { get; protected set; }

        public GetIntelligentBillboardRequest()
        {

        }

        public GetIntelligentBillboardRequest(int timePeriod, int bigRooms, int smallRooms, bool basedOnCity)
        {
            TimePeriod = timePeriod;
            BigRooms = bigRooms;
            SmallRooms = smallRooms;
            BasedOnCity = basedOnCity;
        }
    }
}
