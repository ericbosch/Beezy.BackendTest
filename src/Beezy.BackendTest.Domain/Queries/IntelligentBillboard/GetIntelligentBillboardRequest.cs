using MediatR;

namespace Beezy.BackendTest.Domain.Queries.IntelligentBillboard
{
    public class GetIntelligentBillboardRequest : IRequest<GetIntelligentBillboardResponse>
    {
        public int TimePeriod { get; protected set; }
        public int BigRooms { get; protected set; }
        public int SmallRooms { get; protected set; }
        public string City { get; protected set; }

        public GetIntelligentBillboardRequest()
        {

        }

        public GetIntelligentBillboardRequest(int timePeriod, int bigRooms, int smallRooms, string city)
        {
            TimePeriod = timePeriod;
            BigRooms = bigRooms;
            SmallRooms = smallRooms;
            City = city;
        }
    }
}
